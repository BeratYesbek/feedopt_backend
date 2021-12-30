using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Business.Abstracts
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [LogAspect(typeof(FileLogger))]
        [ValidationAspect(typeof(ProductValidator))]
        //[SecuredOperation("Product.Add,Admin")]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IProductService.GetAll")]
        public IDataResult<Product> Add(Product product)
        {
            return new SuccessDataResult<Product>(_productDal.Add(product), "");
        }


        [LogAspect(typeof(FileLogger))]
        [ValidationAspect(typeof(ProductValidator))]
        [SecuredOperation("Product.Update,Admin")]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IProductService.GetAll")]
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult();
        }

        [LogAspect(typeof(FileLogger))]
        [SecuredOperation("Product.Delete,Admin")]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IProductService.GetAll")]
        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult();
        }

        [LogAspect(typeof(FileLogger))]
        [SecuredOperation("Product.Get,User")]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IProductService.GetAll")]
        public IDataResult<Product> Get(int id)
        {
            var data = _productDal.Get(p => p.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<Product>(data);
            }

            return new ErrorDataResult<Product>(null);
        }

        [LogAspect(typeof(FileLogger))]
        [SecuredOperation("Product.GetAll,User")]
        [PerformanceAspect(5)]
        [CacheRemoveAspect("IProductService.GetAll")]
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