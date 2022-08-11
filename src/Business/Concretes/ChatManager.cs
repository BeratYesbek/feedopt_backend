using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspect.SecurityAspect;
using Business.Security.Role;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.Concretes;
using Entity.Dtos;

namespace Business.Concretes
{
    public class ChatManager : IChatService
    {
        private readonly IChatDal _chatDal;
        private readonly IMapper _mapper;

        public ChatManager(IChatDal chatDal, IMapper mapper)
        {
            _chatDal = chatDal;
            _mapper = mapper;
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.ChatAdd}", Priority = 1)]
        [ValidationAspect(typeof(ChatValidator), Priority = 2)]
        [PerformanceAspect(5, Priority = 5)]
        [LogAspect(typeof(DatabaseLogger), Priority = 6)]
        public async Task<IDataResult<Chat>> Add(Chat chat)
        {
            var data = await _chatDal.AddAsync(chat);
            return new SuccessDataResult<Chat>(data);
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.ChatGetAll}", Priority = 1)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        public async Task<IDataResult<List<ChatDto>>> UpdateChatList(List<ChatDto> list)
        {
            foreach (var item in list)
            {
                item.Chat.IsSeen = true;
                var chat = item.Chat;
                await _chatDal.UpdateAsync(chat);
            }
            return new SuccessDataResult<List<ChatDto>>(list);
        }

        public async Task<IDataResult<Chat>> UpdateChat(Chat chat)
        {
            chat.IsSeen = true;
            await _chatDal.UpdateAsync(chat);
            return new SuccessDataResult<Chat>(chat);
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.ChatGetAll}", Priority = 1)]
        [PerformanceAspect(5, Priority = 3)]
        [LogAspect(typeof(DatabaseLogger), Priority = 4)]
        public async Task<IDataResult<List<ChatDto>>> GetAllByReceiverIdAndSenderId(int senderId, int receiverId)
        {
            var data = await _chatDal.GetAllByReceiverIdAndSenderId(c => c.SenderId == senderId && c.ReceiverId == receiverId
                                                                   || c.SenderId == receiverId &&
                                                                   c.ReceiverId == senderId);

            if (data.Count > 0)
            {
                return new SuccessDataResult<List<ChatDto>>(data);
            }

            return new ErrorDataResult<List<ChatDto>>(null);
        }

        [SecuredOperation($"{Role.Admin},{Role.User},{Role.SuperAdmin},{Role.ChatGetAll}", Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        [PerformanceAspect(5, Priority = 3)]
        public async Task<IDataResult<List<ChatDto>>> GetAllLastMessages(int id)
        {
            var data = await _chatDal.GetAllLastMessages(c => c.ReceiverId == id || c.SenderId == id, id);

            if (data.Count > 0)
            {
                return new SuccessDataResult<List<ChatDto>>(data);
            }

            return new ErrorDataResult<List<ChatDto>>(data);
        }
    }
}