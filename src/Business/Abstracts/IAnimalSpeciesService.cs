using System.Collections.Generic;
using Core.Utilities.Result.Abstracts;
using Entity.concretes;

namespace Business.Abstracts
{
    public interface IAnimalSpeciesService
    {
        IResult Add(AnimalSpecies animalSpecies);

        IResult Update(AnimalSpecies animalSpecies);

        IResult Delete(AnimalSpecies animalSpecies);

        IDataResult<List<AnimalSpecies>> GetAllByAnimalCategoryId(int animalCategoryId);

        IDataResult<AnimalSpecies> Get(int id);

        IDataResult<List<AnimalSpecies>> GetAll();
    }
}