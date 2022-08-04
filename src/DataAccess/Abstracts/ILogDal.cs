using Core.DataAccess;
using Core.Entity.Concretes;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace DataAccess.Abstracts
{
    public interface ILogDal : IEntityRepository<Log>
    {
        List<LogReadDto> GetLogDetails(Expression<Func<Log,bool>> filter = null);

    }
}
