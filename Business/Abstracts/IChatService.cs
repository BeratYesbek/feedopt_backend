using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.concretes;
using Entity.Concretes;
using Entity.Dtos;

namespace Business.Abstracts
{
    public interface IChatService
    {
        IDataResult<Chat> Add(Chat chat);

        IResult Update(Chat chat);

        IResult Delete(Chat chat);

        IDataResult<Chat> Get(int chat);

        IDataResult<List<Chat>> GetAll();

        IDataResult<List<ChatDto>> GetAllByReceiverIdAndSenderId(int senderId, int receiverId);
    }
}