using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using DataAccess.Abstracts;
using Entity.Concretes;

namespace Business.Abstracts
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IDataResult<Product> Add(Product product)
        {
            return new SuccessDataResult<Product>(_productDal.Add(product), "");
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult();
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult();
        }

        public IDataResult<Product> Get(int id)
        {
            var data = _productDal.Get(p => p.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<Product>(data);
            }

            return new ErrorDataResult<Product>(null);
        }

        public IDataResult<List<Product>> GetAll()
        {
            var data = _productDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<Product>>(data);
            }

            return new ErrorDataResult<List<Product>>(data);
        }
    }
}