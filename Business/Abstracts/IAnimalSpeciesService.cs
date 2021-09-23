using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.concretes;

namespace Business.Abstracts
{
    public interface IAnimalSpeciesService
    {
        IResult Add(AnimalSpecies animalSpecies);

        IResult Update(AnimalSpecies animalSpecies);

        IResult Delete(AnimalSpecies animalSpecies);

        IDataResult<AnimalSpecies> Get(int id);

        IDataResult<List<AnimalSpecies>> GetAll();
    }
}