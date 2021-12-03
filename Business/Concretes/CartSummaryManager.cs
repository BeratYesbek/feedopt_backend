using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
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

        public IDataResult<CartSummary> Add(CartSummary cartSummary)
        {
            var data = _cartSummaryDal.Add(cartSummary);
            return new SuccessDataResult<CartSummary>(data);
        }

        public IResult Update(CartSummary cartSummary)
        {
            _cartSummaryDal.Update(cartSummary);
            return new SuccessResult();
        }

        public IResult Delete(CartSummary cartSummary)
        {
            _cartSummaryDal.Delete(cartSummary);
            return new ErrorResult();
        }

        public IDataResult<CartSummary> Get(int id)
        {
            var data = _cartSummaryDal.Get(c => c.Id == id);
            if (data != null)
            {
                return new SuccessDataResult<CartSummary>(data);
            }

            return new ErrorDataResult<CartSummary>(null);
        }

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