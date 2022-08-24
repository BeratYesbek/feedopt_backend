using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess;
using Entity.Concretes;
using Entity.Dtos;

namespace DataAccess.Abstracts
{
    public interface IChatDal : IEntityRepository<Chat>
    {
        Task<List<ChatDto>> GetAllByReceiverIdAndSenderId(Expression<Func<Chat, bool>> filter);

        Task<List<ChatDto>> GetAllLastMessages(Expression<Func<Chat, bool>> filter,int id);
    }
}   