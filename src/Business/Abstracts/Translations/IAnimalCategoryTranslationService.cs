using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes.Translations;

namespace Business.Abstracts.Translations
{
    public interface IAnimalCategoryTranslationService
    {
        IDataResult<AnimalCategoryTranslation> Add(AnimalCategoryTranslation translation);

        IResult Update(AnimalCategoryTranslation translation);

        IResult Delete(AnimalCategoryTranslation translation);

        IDataResult<List<AnimalCategoryTranslation>> GetAll();
    }
}
