using Core.DataAccess;
using Entity.Concretes;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstracts
{
    public interface IFilterDal : IEntityRepository<Filter>
    {
        List<FilterDto> GetByFilterType(Expression<Func<Filter, bool>> filter);
    }
}
