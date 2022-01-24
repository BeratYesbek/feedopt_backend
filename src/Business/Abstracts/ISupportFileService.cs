using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;

namespace Business.Abstracts
{
    public interface ISupportFileService
    {
        IDataResult<SupportFile> Add(SupportFile ticketFile);

        IResult Update(SupportFile ticketFile);

        IResult Delete(SupportFile ticketFile);

        IDataResult<SupportFile> Get(int id);

        IDataResult<List<SupportFile>> GetAll();
    }
}
