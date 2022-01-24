using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.Concretes;

namespace Business.Concretes
{
    public class SupportFileManager : ISupportFileService
    {
        private readonly ITicketFileDal _ticketFileDal;

        public SupportFileManager(ITicketFileDal ticketFileDal)
        {
            _ticketFileDal = ticketFileDal;
        }

        //[SecuredOperation("TicketFile.Add,User")]
        [ValidationAspect(typeof(SupportFileValidator))]
        [CacheRemoveAspect("ITicketFileService.GetAll")]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IDataResult<SupportFile> Add(SupportFile ticketFile)
        {
            var data = _ticketFileDal.Add(ticketFile);
            return new SuccessDataResult<SupportFile>(data);
        }

        [SecuredOperation("TicketFile.Update,User")]
        [ValidationAspect(typeof(SupportFileValidator))]
        [CacheRemoveAspect("ITicketFileService.GetAll")]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IResult Update(SupportFile ticketFile)
        {
            _ticketFileDal.Update(ticketFile);
            return new SuccessResult();
        }

        [SecuredOperation("TicketFile.Delete,User")]
        [CacheRemoveAspect("ITicketFileService.GetAll")]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IResult Delete(SupportFile ticketFile)
        {
            _ticketFileDal.Delete(ticketFile);
            return new SuccessResult();
        }

        [SecuredOperation("TicketFile.Get,User")]
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IDataResult<SupportFile> Get(int id)
        {
            var data = _ticketFileDal.Get(t => t.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<SupportFile>(data);
            }

            return new ErrorDataResult<SupportFile>(null);
        }


        [SecuredOperation("TicketFile.GetAll,User")]
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IDataResult<List<SupportFile>> GetAll()
        {
            var data = _ticketFileDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<SupportFile>>(data);
            }

            return new ErrorDataResult<List<SupportFile>>(null);
        }
    }
}