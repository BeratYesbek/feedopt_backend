using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;

namespace Business.Abstracts
{
    public interface ITokenService
    {
        Task<IDataResult<Token>> Add(Token token); 
        Task<IDataResult<Token>> GetByCurrentUser();
        Task<IDataResult<Token>> FindByTokenAndCurrentUser(Token token);
    }
}
