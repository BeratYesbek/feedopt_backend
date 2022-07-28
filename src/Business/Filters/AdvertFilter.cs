using System;
using System.Linq;
using System.Linq.Expressions;
using Core.Entity.Concretes;
using Core.Utilities.Calculator;
using Entity.Concretes;

namespace Business.Filters
{
    /// <summary>
    /// Advert Filter must contains everything about advert filter because of SOLID design principles
    /// </summary>
    public class AdvertFilter : BaseFilterInvoke
    {
        /// <summary>
        /// This method should use whenever someone wants to filter adverts by advert category ID.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="value"></param>
        /// <returns>Expression</returns>
        protected Expression<Func<Advert, bool>> AdvertCategoryIdCondition(Expression<Func<Advert, bool>> filter, int[] value)
        {
            return filter.And(t => value.Contains(t.AdvertCategoryId));
        }

        /// <summary>
        /// This method should use whenever someone wants to filter adverts by Animal Species ID.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="value"></param>
        /// <returns>Expression</returns>
        protected Expression<Func<Advert, bool>> AnimalSpeciesIdCondition(Expression<Func<Advert, bool>> filter, int[] value)
        {
            return filter.And(c => value.Contains(c.AnimalSpeciesId));
        }

        /// <summary>
        /// This method should use whenever someone wants to filter adverts by Animal Category ID.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="value"></param>
        /// <returns>Expression</returns>
        protected Expression<Func<Advert, bool>> AnimalCategoryIdCondition(Expression<Func<Advert, bool>> filter, int[] value)
        {
            return filter.And(c => value.Contains(c.AnimalCategoryId));
        }

        /// <summary>
        /// This method should use whenever someone wants to filter adverts by User ID.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="value"></param>
        /// <returns>Expression</returns>
        protected Expression<Func<Advert, bool>> UserIdCondition(Expression<Func<Advert, bool>> filter, int value)
        {
            return filter.And(c => c.UserId == value);
        }

        /// <summary>
        /// This method should use whenever someone wants to filter adverts by Age ID.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="value"></param>
        /// <returns>Expression</returns>
        protected Expression<Func<Advert, bool>> AgeIdCondition(Expression<Func<Advert, bool>> filter, int[] value)
        {
            return filter.And(c => value.Contains(c.AgeId));
        }

        /// <summary>
        /// This method should use whenever someone wants to filter adverts by Color ID.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="value"></param>
        /// <returns>Expression</returns>
        protected Expression<Func<Advert, bool>> ColorIdCondition(Expression<Func<Advert, bool>> filter, int[] value)
        {
            return filter.And(c => value.Contains(c.ColorId));
        }

        /// <summary>
        /// This method should use whenever someone wants to filter adverts by Gender.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="value"></param>
        /// <returns>Expression</returns>
        protected Expression<Func<Advert, bool>> GenderCondition(Expression<Func<Advert, bool>> filter, Gender[] value)
        {
            return filter.And(c => value.Contains(c.Gender));
        }

        protected Expression<Func<Advert, bool>> StatusCondition(Expression<Func<Advert, bool>> filter, Status[] value)
        {
            return filter.And(c => value.Contains(c.Status));
        }

   
        

    }
}

