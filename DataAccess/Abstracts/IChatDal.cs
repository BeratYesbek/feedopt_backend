using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;
using Entity.Dtos;

namespace DataAccess.Abstracts
{
    public interface IChatDal : IEntityRepository<Chat>
    {
        List<ChatDto> GetAllByReceiverIdAndSenderId(Expression<Func<Chat, bool>> filter);
    }
}   