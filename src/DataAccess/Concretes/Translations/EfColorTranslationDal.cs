using Core.DataAccess;
using DataAccess.Abstracts.Translations;
using Entity.Concretes.Translations;

namespace DataAccess.Concretes.Translations
{
    public class EfColorTranslationDal : EfEntityRepositoryBase<ColorTranslation, AppDbContext>, IColorTranslationDal
    {
    }
}
