using System.Collections.Generic;
using Core.Utilities.Result.Abstracts;
using Entity.concretes;

namespace Business.Abstracts
{
    public interface IAnimalCategoryService
    {
        IDataResult<AnimalCategory> Add(AnimalCategory animalCategory);

        IResult Update(AnimalCategory animalCategory);

        IResult Delete(AnimalCategory animalCategory);

        IDataResult<AnimalCategory> Get(int id);

        IDataResult<List<AnimalCategory>> GetAll( );
    }
}
