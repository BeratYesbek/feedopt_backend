using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Extensions;

namespace Core.Utilities.Calculator
{
    public class Calculator
    {
        public static double CalculateDistance(double latitude1, double longitude1,double latitude2,double longitude2)
        {
            var distance = new Coordinates(latitude1, longitude1).DistanceTo(new Coordinates(latitude2, longitude2),
                UnitOfLength.Kilometers);
            return distance;
        }
    }
}
