using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Concretes;
using Entity.concretes;
using System;
using System.Collections.Generic;

namespace Business.Concretes
{
    public class AnimalCategoryManager : IAnimalCategoryService
    {
        EfAnimalCategoryDal animalCategoryDal = new EfAnimalCategoryDal();

        public IResult Add(AnimalCategory animalCategory)
        {
            animalCategoryDal.Add(animalCategory);
            return new SuccessResult();
        }

        public IResult Update(AnimalCategory animalCategory)
        {
            animalCategoryDal.Update(animalCategory);
            return new SuccessResult();
        }

        public IResult Delete(AnimalCategory animalCategory)
        {
            animalCategoryDal.Delete(animalCategory);
            return new SuccessResult();
        }

        public IDataResult<AnimalCategory> Get(int id)
        {
            var data = animalCategoryDal.Get(a => a.AnimalCategoryId == id);
            if (data != null)
            {
                return new SuccessDataResult<AnimalCategory>(data);
            }

            return new ErrorDataResult<AnimalCategory>(null);
        }

        public IDataResult<List<AnimalCategory>> GetAll()
        {
            var data = animalCategoryDal.GetAll();

            if (data.Count > 0)
            {
                return new SuccessDataResult<List<AnimalCategory>>(data);
            }

            return new ErrorDataResult<List<AnimalCategory>>(null);
        }
    }
}