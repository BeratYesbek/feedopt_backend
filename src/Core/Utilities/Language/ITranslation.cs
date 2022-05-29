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
        string CultureName { get; set; }

        string PropertyName { get; set; }

        string Content { get; set; }
    }
}
