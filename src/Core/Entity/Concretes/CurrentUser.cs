using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity.Concretes
{
    public static class CurrentUser
    {
        public static User User { get; set; }

        public static double Latitude { get; set; } = default;

        public static double Longitude { get; set; } = default;
    }
}