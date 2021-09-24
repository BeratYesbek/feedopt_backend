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
        private EfAnimalSpeciesDal animalSpeciesDal = new EfAnimalSpeciesDal();

        public IResult Add(AnimalSpecies animalSpecies)
        {
            animalSpeciesDal.Add(animalSpecies);
            return new SuccessResult();
        }

        public IResult Update(AnimalSpecies animalSpecies)
        {
            animalSpeciesDal.Update(animalSpecies);
            return new SuccessResult();
        }

        public IResult Delete(AnimalSpecies animalSpecies)
        {
            animalSpeciesDal.Delete(animalSpecies);
            return new SuccessResult();
        }

        public IDataResult<AnimalSpecies> Get(int id)
        {
            var data = animalSpeciesDal.Get(a => a.AnimalSpeciesId == id);
            if (data != null)
            {
                return new SuccessDataResult<AnimalSpecies>(data);
            }

            return new ErrorDataResult<AnimalSpecies>(null);
        }

        public IDataResult<List<AnimalSpecies>> GetAll()
        {
            var data = animalSpeciesDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AnimalSpecies>>(data);
            }

            return new ErrorDataResult<List<AnimalSpecies>>(null);
        }
    }
}