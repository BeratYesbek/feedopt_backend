using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Messages
{
    public class AdvertValidationMessages
    {
        public static string AdvertUserIdEmptyMessage = "User cannot be blank";

        public static string AdvertDescriptionLengthMessage = "Description cannot be less than 100 character and greater than 500 character";

        public static string AdvertEmptyDescriptionMessage = "Description cannot be blank";

        public static string AdvertEmptyLocationIdMessage = "Location cannot be blank";


    }
}
