using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
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

        public ChatManager(IChatDal chatDal)
        {
            _chatDal = chatDal;
        }

        //[SecuredOperation("Chat.Add,User")]
       // [ValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect("IChatService.GetAllByReceiverIdAndSenderId")]
        [PerformanceAspect(5)]
      //  [LogAspect(typeof(FileLogger))]
        public IDataResult<Chat> Add(Chat chat)
        {
            return new SuccessDataResult<Chat>(_chatDal.Add(chat));
        }

        //[SecuredOperation("Chat.Update,User")]
       // [ValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect("IChatService.GetAllByReceiverIdAndSenderId")]
        [PerformanceAspect(5)]
       // [LogAspect(typeof(FileLogger))]
        public IResult Update(Chat chat)
        {
            _chatDal.Update(chat);
            return new SuccessResult();
        }

       // [SecuredOperation("Chat.Delete,User")]
        [ValidationAspect(typeof(ChatValidator))]
        [CacheRemoveAspect("IChatService.GetAllByReceiverIdAndSenderId")]
       // [PerformanceAspect(5)]
        //[LogAspect(typeof(FileLogger))]
        public IResult Delete(Chat chat)
        {
            _chatDal.Delete(chat);
            return new SuccessResult();
        }

       // [SecuredOperation("Chat.Get,User")]
        [CacheRemoveAspect("IChatService.GetAllByReceiverIdAndSenderId")]
        [PerformanceAspect(5)]
       // [LogAspect(typeof(FileLogger))]
        public IDataResult<Chat> Get(int id)
        {
            var data = _chatDal.Get(c => c.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<Chat>(data);
            }

            return new ErrorDataResult<Chat>(null);
        }

        //[SecuredOperation("Chat.GetAll,User")]
        [CacheRemoveAspect("IChatService.GetAllByReceiverIdAndSenderId")]
        [PerformanceAspect(5)]
       // [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Chat>> GetAll()
        {
            var data = _chatDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<Chat>>(data);
            }

            return new ErrorDataResult<List<Chat>>(null);
        }

        public IDataResult<List<ChatDto>> GetAllLastMessagesByUserId()
        {
            return null;
        }

        //[SecuredOperation("Chat.GetAllByReceiverIdAndSenderId,User")]
        [CacheRemoveAspect("IChatService.GetAllByReceiverIdAndSenderId")]
        [PerformanceAspect(5)]
       // [LogAspect(typeof(FileLogger))]
        public IDataResult<List<ChatDto>> GetAllByReceiverIdAndSenderId(int senderId, int receiverId)
        {
            var data = _chatDal.GetAllByReceiverIdAndSenderId(c => c.SenderId == senderId && c.ReceiverId == receiverId
                                                                   || c.SenderId == receiverId &&
                                                                   c.ReceiverId == senderId);

            if (data.Count > 0)
            {
                return new SuccessDataResult<List<ChatDto>>(data);
            }

            return new ErrorDataResult<List<ChatDto>>(null);
        }

        //[SecuredOperation("Chat.GetAllLastMessages,User")]
        [CacheRemoveAspect("IChatService.GetAllLastMessages")]
        [PerformanceAspect(5)]
        //[LogAspect(typeof(FileLogger))]
        public IDataResult<List<ChatDto>> GetAllLastMessages(int id)
        {



            var data = _chatDal.GetAllLastMessages(c => c.ReceiverId == id || c.SenderId == id, id);

            if (data.Count > 0)
            {
                return new SuccessDataResult<List<ChatDto>>(data);
            }

            return new ErrorDataResult<List<ChatDto>>(data);
        }
    }
}