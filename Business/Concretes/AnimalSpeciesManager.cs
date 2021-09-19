using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.concretes;

namespace Business.Abstracts
{
    public class AnimalSpeciesManager : IAnimalSpeciesService
    {
        public IResult Add(AnimalSpecies animalSpecies)
        {
            throw new NotImplementedException();
        }

        public IResult Update(AnimalSpecies animalSpecies)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(AnimalSpecies animalSpecies)
        {
            throw new NotImplementedException();
        }

        public IDataResult<AnimalSpecies> Get(AnimalSpecies animalSpecies)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<AnimalSpecies>> GetAll(AnimalSpecies animalSpecies)
        {
            throw new NotImplementedException();
        }
    }
}
