using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.concretes;
using Entity.Concretes;

namespace Business.Abstracts
{
    public interface ISupportService 
    {
        IDataResult<Support> Add(Support ticket);

        IResult Update(Support ticket);

        IResult Delete(Support ticket);

        IDataResult<Support> Get(int id);

        IDataResult<List<Support>> GetAll();
    }
}
