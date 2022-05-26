using System;
using System.Linq;
using System.Linq.Expressions;
using Core.Entity.Concretes;
using Core.Utilities.Calculator;
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

        protected Expression<Func<Advert, bool>> AgeIdCondition(Expression<Func<Advert, bool>> filter, int[] value)
        {
            return filter.And(c => value.Contains(c.AgeId));
        }
         
        protected Expression<Func<Advert, bool>> ColorIdCondition(Expression<Func<Advert, bool>> filter, int[] value)
        {
            return filter.And(c => value.Contains(c.ColorId));
        }

        protected Expression<Func<Advert, bool>> GenderCondition(Expression<Func<Advert, bool>> filter, Gender[] value)
        {
            return filter.And(c => value.Contains(c.Gender));
        }

   
        

    }
}

