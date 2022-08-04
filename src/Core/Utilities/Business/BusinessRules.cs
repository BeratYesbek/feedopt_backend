using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            foreach (var item in logics)
            {
                if (!item.Success)
                {
                    return item;
                }
            }

            return new SuccessResult();
        }
    }
}
