using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts.Translations;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts.Translations;
using Entity.Concretes.Translations;

namespace Business.Concretes.Translations
{
    public class AnimalCategoryTranslationManager : IAnimalCategoryTranslationService
    {
        private readonly IAnimalCategoryTranslationDal _animalCategoryTranslationDal;
        public AnimalCategoryTranslationManager(IAnimalCategoryTranslationDal animalCategoryTranslationDal)
        {
            _animalCategoryTranslationDal = animalCategoryTranslationDal;
        }
        public IDataResult<AnimalCategoryTranslation> Add(AnimalCategoryTranslation translation)
        {
            var data = _animalCategoryTranslationDal.Add(translation);
            return new SuccessDataResult<AnimalCategoryTranslation>(data);
        }

        public IResult Update(AnimalCategoryTranslation translation)
        {
            _animalCategoryTranslationDal.Update(translation);
            return new SuccessResult();
        }

        public IResult Delete(AnimalCategoryTranslation translation)
        {
            _animalCategoryTranslationDal.Delete(translation);
            return new SuccessResult();
        }

        public IDataResult<List<AnimalCategoryTranslation>> GetAll()
        {
            return new SuccessDataResult<List<AnimalCategoryTranslation>>(_animalCategoryTranslationDal.GetAll());
        }
    }
}
