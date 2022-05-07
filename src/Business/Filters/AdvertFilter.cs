using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Extensions;
using Entity.Concretes;
using FluentEmail.Core;

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
            return filter.And(c => value.Contains(c.AnimalSpecies.AnimalCategoryId));
        }

        protected Expression<Func<Advert, bool>> UserIdCondition(Expression<Func<Advert, bool>> filter, int value)
        {
            return filter.And(c => c.UserId == value);
        }


    }
}
