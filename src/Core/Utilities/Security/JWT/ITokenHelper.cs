using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user,List<OperationClaim> operationClaims, DateTime dateTime = default, TokenType tokenType = default);
        /// <summary>
        /// Message Contains Identifier of User
        /// </summary>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        IDataResult<dynamic> GetIdentifier(string tokenType);
    }
}
