using Business.Abstracts;
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class LogManager : ILogService
    {
        private readonly ILogDal _logDal;

        public LogManager(ILogDal logDal)
        {
            _logDal = logDal;
        }

        public IDataResult<List<Log>> GetAll()
        {
            return new SuccessDataResult<List<Log>>(_logDal.GetAll());
        }

        public IDataResult<List<LogReadDto>> GetLogDetails()
        {
            return new SuccessDataResult<List<LogReadDto>>(_logDal.GetLogDetails());
        }
    }
}
