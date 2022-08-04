using Core.Utilities.Result.Abstracts;
using Entity.Concretes;
using Entity.Dtos;
using System.Collections.Generic;


namespace Business.Abstracts
{
    public interface IFilterService
    {
        IDataResult<Filter> Add(Filter filter);

        IResult Update(Filter filter);

        IResult Delete(Filter filter);

        IDataResult<Filter> GetById(int id);

        IDataResult<List<FilterDto>> GetByFilterType(string type);

        IDataResult<List<Filter>> GetAll();
    }
}
