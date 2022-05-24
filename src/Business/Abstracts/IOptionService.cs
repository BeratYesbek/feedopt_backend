using Core.Utilities.Result.Abstracts;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IOptionService
    {
        IDataResult<OptionDto> GetOptions();
    }
}
