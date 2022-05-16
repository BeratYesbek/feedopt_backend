using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Concretes;
using Core.Utilities.Result.Abstracts;

namespace Business.Abstracts
{
    public interface ITranslationService
    {
        IDataResult<Translation> Add(Translation translation);

        IResult Update(Translation translation);

        IResult Delete(Translation translation);

        IDataResult<Translation> GetById(int id);

        IDataResult<List<Translation>> GetByType(string type);

        IDataResult<List<Translation>> GetAll();
    }
}
