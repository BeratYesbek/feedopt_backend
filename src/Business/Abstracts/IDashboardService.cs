using Core.Utilities.Result.Abstracts;
using Entity.Dtos;

namespace Business.Abstracts
{
    public interface IDashboardService
    {
        IDataResult<DashboardDto> GetDashboard();
    }
}
