using Core.Entity.Concretes;
using Core.Utilities.Calculator;
using Entity.Concretes;
using System;
using System.Linq.Expressions;


namespace Business.Filters
{
    /// <summary>
    /// Location filter must contains everything about advert filter because of SOLID design principles
    /// </summary>
    public class LocationFilter : BaseFilterInvoke
    {
        /// <summary>
        /// This method filter by distance
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        protected Expression<Func<Location, bool>> DistanceCondition(Expression<Func<Location, bool>> filter, int distance)
        {
            return filter.And(c => (int)Calculator.CalculateDistance(CurrentUser.Latitude, CurrentUser.Longitude,c.Latitude, c.Longitude) < distance);
        }

    }
}
