using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entity.Concretes;

namespace Business.Filters
{
    public class AdvertFilter : BaseFilterInvoke
    {

        protected  Expression<Func<Advert, bool>> AdvertCategoryIdCondition(Expression<Func<Advert, bool>> filter, int value)
        {
            return filter.And(c => c.AdvertCategoryId == value);
        }

        protected Expression<Func<Advert, bool>> AnimalSpeciesIdCondition(Expression<Func<Advert, bool>> filter, int value)
        {
            return filter.And(c => c.AnimalSpeciesId == value);
        }

        protected Expression<Func<Advert, bool>> UserIdCondition(Expression<Func<Advert, bool>> filter, int value)
        {
            return filter.And(c => c.UserId == value);
        }
    }
}
