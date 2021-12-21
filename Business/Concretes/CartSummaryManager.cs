using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
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

namespace Business.Concretes
{
    public class CartSummaryManager : ICartSummaryService
    {
        private readonly ICartSummaryDal _cartSummaryDal;

        public CartSummaryManager(ICartSummaryDal cartSummaryDal)
        {
            _cartSummaryDal = cartSummaryDal;
        }

        [SecuredOperation("CartSummary.Add,User")]
        [ValidationAspect(typeof(CartSummaryValidator))]
        [CacheRemoveAspect("ICartSummaryService.GetAll")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<CartSummary> Add(CartSummary cartSummary)
        {
            var data = _cartSummaryDal.Add(cartSummary);
            return new SuccessDataResult<CartSummary>(data);
        }

        [SecuredOperation("CartSummary.Update,User")]
        [ValidationAspect(typeof(CartSummaryValidator))]
        [CacheRemoveAspect("ICartSummaryService.GetAll")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(FileLogger))]
        public IResult Update(CartSummary cartSummary)
        {
            _cartSummaryDal.Update(cartSummary);
            return new SuccessResult();
        }

        [SecuredOperation("CartSummary.Delete,User")]
        [CacheRemoveAspect("ICartSummaryService.GetAll")]
        [PerformanceAspect(5)]
        [LogAspect(typeof(FileLogger))]
        public IResult Delete(CartSummary cartSummary)
        {
            _cartSummaryDal.Delete(cartSummary);
            return new ErrorResult();
        }


        [SecuredOperation("CartSummary.Get,User")]
        [CacheAspect]
        [PerformanceAspect(5)]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<CartSummary> Get(int id)
        {
            var data = _cartSummaryDal.Get(c => c.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<CartSummary>(data);
            }

            return new ErrorDataResult<CartSummary>(null);
        }

        [SecuredOperation("CartSummary.GetAll,User")]
        [CacheAspect]
        [PerformanceAspect(5)]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<CartSummary>> GetAll()
        {
            var data = _cartSummaryDal.GetAll();
            if (data.Count > 0)
            {
                return new SuccessDataResult<List<CartSummary>>(data);
            }

            return new ErrorDataResult<List<CartSummary>>(null);
        }
    }
}