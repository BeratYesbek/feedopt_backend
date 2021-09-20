using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Concretes;
using Entity.concretes;

namespace Business.Concretes
{
    public class AnimalCategoryManager : IAnimalCategoryService
    {
        public IResult Add(AnimalCategory animalCategory)
        {
            EfAnimalCategoryDal a = new EfAnimalCategoryDal();
            a.Add(animalCategory);
            return new SuccessResult();
        }

        public IResult Update(AnimalCategory animalCategory)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(AnimalCategory animalCategory)
        {
            throw new NotImplementedException();
        }

        public IDataResult<AnimalCategory> Get(AnimalCategory animalCategory)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<AnimalCategory>> GetAll(AnimalCategory animalCategory)
        {
            throw new NotImplementedException();
        }
    }
}