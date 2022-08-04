using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;
using Entity.Dtos;
using System.Collections.Generic;

namespace Business.Abstracts
{
    public interface ILogService
    {
        IDataResult<List<Log>> GetAll();

        IDataResult<List<LogReadDto>> GetLogDetails();
    }
}
