using System.Collections.Generic;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;

namespace Business.Abstracts
{
    public interface IAgeRangeService
    {
        IResult Add(Age age);

        IResult Update(Age age);

        IResult Delete(Age age);

        IDataResult<Age> Get(int id);

        IDataResult<List<Age>> GetAll();
    }
}
