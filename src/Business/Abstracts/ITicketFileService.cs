using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;

namespace Business.Abstracts
{
    public interface ITicketFileService
    {
        IDataResult<TicketFile> Add(TicketFile ticketFile);

        IResult Update(TicketFile ticketFile);

        IResult Delete(TicketFile ticketFile);

        IDataResult<TicketFile> Get(int id);

        IDataResult<List<TicketFile>> GetAll();
    }
}
