using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity;
using Entity.Concretes;

namespace Business.Abstracts
{
    public interface IProductService
    {
        IDataResult<Product> Add(Product product);

        IResult Update(Product product);

        IResult Delete(Product product);

        IDataResult<Product> Get(int id);

        IDataResult<List<Product>> GetAll();
    }
}