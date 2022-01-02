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
    public class TicketManager : ITicketService
    {
        private readonly ITicketDal _ticketDal;
        private readonly ITicketFileService _ticketFileService;
        public TicketManager(ITicketDal ticketDal, ITicketFileService ticketFileService)
        {
            _ticketDal = ticketDal;
            _ticketFileService = ticketFileService;
        }


        //[SecuredOperation("Ticket.Add,User")]
        [ValidationAspect(typeof(TicketValidator))]
        [CacheRemoveAspect("ITicketService.GetAll")]
        [LogAspect(typeof(FileLogger))]
        [MailerAspect(typeof(TicketEmailMailer), EmailType.TicketEmail)]
        [PerformanceAspect(5)]
        public IDataResult<Ticket> Add(Ticket ticket)
        {
            var data = _ticketDal.Add(ticket);
            foreach (var file in ticket.FormFiles)
            {
                var formFiles = new FileHelper(RecordType.Cloud, FileExtension.DocumentExtension);
                var result = formFiles.Upload(file);
                if (!result.Success)
                {
                    return new ErrorDataResult<Ticket>(null, result.Message);
                }

                // result.message contains fileUrl and publicId
                var fileUrl = result.Message.Split("&&")[0];
                var publicId = result.Message.Split("&&")[1];
                var ticketFile = new TicketFile(fileUrl, publicId, data.Id);
                _ticketFileService.Add(ticketFile);
            }

            return new SuccessDataResult<Ticket>(data);
        }

        [SecuredOperation("Ticket.Update,User")]
        [ValidationAspect(typeof(TicketValidator))]
        [CacheRemoveAspect("ITicketService.GetAll")]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IResult Update(Ticket ticket)
        {
            _ticketDal.Update(ticket);
            return new SuccessResult();
        }

        [SecuredOperation("Ticket.Delete,User")]
        [CacheRemoveAspect("ITicketService.GetAll")]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IResult Delete(Ticket ticket)
        {
            _ticketDal.Delete(ticket);
            return new SuccessResult();
        }


        [SecuredOperation("Ticket.Get,User")]
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IDataResult<Ticket> Get(int id)
        {
            var data = _ticketDal.Get(t => t.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<Ticket>(data);
            }

            return new ErrorDataResult<Ticket>(null);
        }

        [SecuredOperation("Ticket.GetAll,User")]
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(5)]
        public IDataResult<List<Ticket>> GetAll()
        {
            var data = _ticketDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<Ticket>>(data);
            }

            return new ErrorDataResult<List<Ticket>>(null);
        }
    }
}