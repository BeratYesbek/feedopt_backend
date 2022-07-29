using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.Dtos;

namespace Business.Abstracts
{
    public interface IDashboardService
    {
        IDataResult<DashboardDto> GetDashboard();
    }
}
