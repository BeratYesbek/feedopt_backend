
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
        public static bool IsLessOrEqualThan(double distance1, double distance2) 
        {
            return (distance1 <= distance2);
        }
    }
}
