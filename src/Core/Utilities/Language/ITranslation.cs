using Core.Entity.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Language
{
    public interface ITranslation
    {
        bool Translate(bool translation);
    }
}
