using Core.DataAccess;
using DataAccess.Abstracts.Translations;
using Entity.Concretes.Translations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EfColorTranslationDal
{
    public class EfColorTranslationDal : EfEntityRepositoryBase<ColorTranslation,AppDbContext>,IColorTranslationDal
    {
    }
}
