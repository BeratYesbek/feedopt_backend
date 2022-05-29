using Core.DataAccess;
using Entity.Concretes.Translations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstracts.Translations
{
    public interface IColorTranslationDal : IEntityRepository<ColorTranslation>
    {
    }
}
