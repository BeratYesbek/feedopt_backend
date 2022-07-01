using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entity.Concretes.Translations;

namespace DataAccess.Abstracts.Translations
{
    public interface IAdvertCategoryTranslationDal : IEntityRepository<AdvertCategoryTranslation>
    {
    }
}
