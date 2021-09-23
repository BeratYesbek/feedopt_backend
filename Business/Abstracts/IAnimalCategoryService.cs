using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.concretes;

namespace Business.Abstracts
{
    public interface IAnimalCategoryService
    {
        IResult Add(AnimalCategory animalCategory);

        IResult Update(AnimalCategory animalCategory);

        IResult Delete(AnimalCategory animalCategory);

        IDataResult<AnimalCategory> Get(int id);

        IDataResult<List<AnimalCategory>> GetAll( );
    }
}
