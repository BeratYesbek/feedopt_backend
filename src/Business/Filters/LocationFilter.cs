using Core.Entity.Concretes;
using Core.Utilities.Calculator;
using Entity.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Filters
{
    public class LocationFilter : BaseFilterInvoke
    {
        protected Expression<Func<Location, bool>> DistanceCondition(Expression<Func<Location, bool>> filter, int distance)
        {
            return filter.And(c => (int)Calculator.CalculateDistance(CurrentUser.Latitude, CurrentUser.Longitude,c.Latitude, c.Longitude) < distance);
        }

    }
}
