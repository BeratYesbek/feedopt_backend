using Business.Abstracts;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts.Translations;
using Entity.Concretes.Translations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes.Translations
{
    public class ColorTranslationManager : IColorTranslationService
    {
        private readonly IColorTranslationDal _colorTranslationDal;
        public ColorTranslationManager(IColorTranslationDal colorTranslationDal)
        {
            _colorTranslationDal = colorTranslationDal;
        }
        public IDataResult<ColorTranslation> Add(ColorTranslation translation)
        {
            ;
            return new SuccessDataResult<ColorTranslation>(_colorTranslationDal.Add(translation));
        }

        public IResult Delete(ColorTranslation translation)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<ColorTranslation>> GetAll()
        {
            return new SuccessDataResult<List<ColorTranslation>>(_colorTranslationDal.GetAll());
        }

        public IResult Update(ColorTranslation translation)
        {
            throw new NotImplementedException();
        }
    }
}
