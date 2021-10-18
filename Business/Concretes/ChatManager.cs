using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
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

        public IDataResult<Chat> Add(Chat chat)
        {
            return new SuccessDataResult<Chat>(_chatDal.Add(chat));
        }

        public IResult Update(Chat chat)
        {
            _chatDal.Update(chat);
            return new SuccessResult();
        }

        public IResult Delete(Chat chat)
        {
            _chatDal.Delete(chat);
            return new SuccessResult();
        }

        public IDataResult<Chat> Get(int id)
        {
            var data = _chatDal.Get(c => c.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<Chat>(data);
            }

            return new ErrorDataResult<Chat>(null);
        }

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
    }
}