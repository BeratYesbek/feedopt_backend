using System.Collections.Generic;
using Business.Abstracts.Translations;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts.Translations;
using Entity.Concretes.Translations;

namespace Business.Concretes.Translations
{
    public class AdvertCategoryTranslationManager : IAdvertCategoryTranslationService
    {
        private readonly IAdvertCategoryTranslationDal _advertCategoryTranslationDal;
        public AdvertCategoryTranslationManager(IAdvertCategoryTranslationDal advertCategoryTranslationDal)
        {
            _advertCategoryTranslationDal = advertCategoryTranslationDal;
        }
        public IDataResult<AdvertCategoryTranslation> Add(AdvertCategoryTranslation translation)
        {
            var data = _advertCategoryTranslationDal.Add(translation);
            return new SuccessDataResult<AdvertCategoryTranslation>(data);
        }

        public IResult Update(AdvertCategoryTranslation translation)
        {
            _advertCategoryTranslationDal.Update(translation);
            return new SuccessResult();
        }

        public IResult Delete(AdvertCategoryTranslation translation)
        {
            _advertCategoryTranslationDal.Delete(translation);
            return new SuccessResult();
        }

        public IDataResult<List<AdvertCategoryTranslation>> GetAll()
        {
            return new SuccessDataResult<List<AdvertCategoryTranslation>>(_advertCategoryTranslationDal.GetAll());
        }
    }
}
