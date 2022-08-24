using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Core.DataAccess;
using Core.Entity.Concretes;
using Entity.Dtos;

namespace DataAccess.Abstracts
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);

        List<UserDto> GetUserDetails(Expression<Func<User,bool>> filter);

    }
}