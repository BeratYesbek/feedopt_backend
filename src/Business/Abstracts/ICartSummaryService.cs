using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;

namespace Business.Abstracts
{
    public interface ICartSummaryService
    {
        IDataResult<CartSummary> Add(CartSummary cartSummary);

        IResult Update(CartSummary cartSummary);

        IResult Delete(CartSummary cartSummary);

        IDataResult<CartSummary> Get(int id);

        IDataResult<List<CartSummary>> GetAll();
    }
}