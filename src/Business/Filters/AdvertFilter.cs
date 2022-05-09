using System;
using System.Linq;
using System.Linq.Expressions;
using Entity.Concretes;

namespace Business.Filters
{
    public class AdvertFilter : BaseFilterInvoke
    {

        protected Expression<Func<Advert, bool>> AdvertCategoryIdCondition(Expression<Func<Advert, bool>> filter, int[] value)
        {
            return filter.And(t => value.Contains(t.AdvertCategoryId));
        }

        protected Expression<Func<Advert, bool>> AnimalSpeciesIdCondition(Expression<Func<Advert, bool>> filter, int[] value)
        {
            return filter.And(c => value.Contains(c.AnimalSpeciesId));
        }

        protected Expression<Func<Advert, bool>> AnimalCategoryIdCondition(Expression<Func<Advert, bool>> filter, int[] value)
        {
            return filter.And(c => value.Contains(c.AnimalCategoryId));
        }

        protected Expression<Func<Advert, bool>> UserIdCondition(Expression<Func<Advert, bool>> filter, int value)
        {
            return filter.And(c => c.UserId == value);
        }


    }
}
