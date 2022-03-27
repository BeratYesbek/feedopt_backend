using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;

namespace Business.Abstracts
{
    public interface IColorService
    {
        IDataResult<Color> Add(Color color);

        IResult Update(Color color);

        IResult Delete(Color color);

        IDataResult<Color> Get(int id);

        IDataResult<List<Color>> GetAll();
    }
}
