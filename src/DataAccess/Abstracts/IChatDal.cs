using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.DataAccess;
using Entity.Concretes;
using Entity.Dtos;

namespace DataAccess.Abstracts
{
    public interface IChatDal : IEntityRepository<Chat>
    {
        List<ChatDto> GetAllByReceiverIdAndSenderId(Expression<Func<Chat, bool>> filter);

        List<ChatDto> GetAllLastMessages(Expression<Func<Chat, bool>> filter,int id);
    }
}   