using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.BusinessMailer;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.FileHelper;
using Core.Utilities.Mailer;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.concretes;
using Entity.Concretes;

namespace Business.Concretes
{
    public class SupportManager : ISupportService
    {
        private readonly ITicketDal _ticketDal;
        private readonly ISupportFileService _ticketFileService;
        public SupportManager(ITicketDal ticketDal, ISupportFileService ticketFileService)
        {
            _ticketDal = ticketDal;
            _ticketFileService = ticketFileService;
        }


        //[SecuredOperation("Ticket.Add,User")]
        [ValidationAspect(typeof(SupportValidator))]
        [CacheRemoveAspect("ISupportFileService.GetAll")]
        [LogAspect(typeof(FileLogger))]
        [MailerAspect(typeof(SupportEmailMailer), EmailType.TicketEmail)]
        [PerformanceAspect(5)]
        public IDataResult<Support> Add(Support ticket)
        {
            var data = _ticketDal.Add(ticket);
            foreach (var file in ticket.FormFiles)
            {
                var formFiles = new FileHelper(RecordType.Cloud, FileExtension.DocumentExtension);
                var result = formFiles.Upload(file);
                if (!result.Success)
                {
                    return new ErrorDataResult<Support>(null, result.Message);
                }

                // result.message contains fileUrl and publicId
                var fileUrl = result.Message.Split("&&")[0];
                var publicId = result.Message.Split("&&")[1];
                var ticketFile = new SupportFile(fileUrl, publicId, data.Id);
                _ticketFileService.Add(ticketFile);
            }

            return new SuccessDataResult<Support>(data);
        }

        [SecuredOperation("Support.Update,User")]
        [ValidationAspect(typeof(SupportValidator))]
        [CacheRemoveAspect("ISupportFileService.GetAll")]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IResult Update(Support ticket)
        {
            _ticketDal.Update(ticket);
            return new SuccessResult();
        }

        [SecuredOperation("Support.Delete,User")]
        [CacheRemoveAspect("ISupportFileService.GetAll")]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IResult Delete(Support ticket)
        {
            _ticketDal.Delete(ticket);
            return new SuccessResult();
        }


        [SecuredOperation("Support.Get,User")]
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IDataResult<Support> Get(int id)
        {
            var data = _ticketDal.Get(t => t.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<Support>(data);
            }

            return new ErrorDataResult<Support>(null);
        }

        [SecuredOperation("Support.GetAll,User")]
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IDataResult<List<Support>> GetAll()
        {
            var data = _ticketDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<Support>>(data);
            }

            return new ErrorDataResult<List<Support>>(null);
        }
    }
}