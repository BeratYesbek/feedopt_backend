using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes.Translations;

namespace Business.Abstracts.Translations
{
    public interface IAdvertCategoryTranslationService
    {
        IDataResult<AdvertCategoryTranslation> Add(AdvertCategoryTranslation translation);

        IResult Update(AdvertCategoryTranslation translation);

        IResult Delete(AdvertCategoryTranslation translation);

        IDataResult<List<AdvertCategoryTranslation>> GetAll();
    }
}
