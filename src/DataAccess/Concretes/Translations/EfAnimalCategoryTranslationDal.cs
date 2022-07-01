using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using DataAccess.Abstracts.Translations;
using Entity.Concretes.Translations;

namespace DataAccess.Concretes.Translations
{
    public class EfAnimalCategoryTranslationDal : EfEntityRepositoryBase<AnimalCategoryTranslation, AppDbContext>, IAnimalCategoryTranslationDal
    {
    }
}
