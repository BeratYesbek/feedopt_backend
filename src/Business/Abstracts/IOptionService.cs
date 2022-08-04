using Core.Utilities.Result.Abstracts;
using Entity.Dtos;

namespace Business.Abstracts
{
    public interface IOptionService
    {
        IDataResult<OptionDto> GetOptions();
    }
}
