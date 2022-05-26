using Core.Utilities.Result.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public interface IRule
    {
        IResult Run();
    }
}
