using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Concretes;
using Entity.concretes;

namespace Business.Abstracts
{
    public class AnimalSpeciesManager : IAnimalSpeciesService
    {
        public IResult Add(AnimalSpecies animalSpecies)
        {
            var addedData = new EfAnimalSpeciesDal();
            addedData.Add(animalSpecies);
            return new SuccessResult();
        }

        public IResult Update(AnimalSpecies animalSpecies)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(AnimalSpecies animalSpecies)
        {
            throw new NotImplementedException();
        }

        public IDataResult<AnimalSpecies> Get(int id)
        {
            var data = new EfAnimalSpeciesDal().Get(a => a.AnimalSpeciesId == id);
            return new SuccessDataResult<AnimalSpecies>(data);
        }
        public IDataResult<List<AnimalSpecies>> GetAll()
        {
            throw new NotImplementedException();

        }
    }
}
