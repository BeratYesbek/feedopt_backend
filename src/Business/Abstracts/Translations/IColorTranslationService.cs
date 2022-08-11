using Core.Utilities.Result.Abstracts;
using Entity.Concretes.Translations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts.Translations
{
    public interface IColorTranslationService
    {
        IDataResult<ColorTranslation> Add(ColorTranslation translation);

        IResult Update(ColorTranslation translation);

        IResult Delete(ColorTranslation translation);

        IDataResult<List<ColorTranslation>> GetAll();
    }
}
