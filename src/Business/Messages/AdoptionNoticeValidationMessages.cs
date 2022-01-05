using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Messages
{
    public class AdoptionNoticeValidationMessages
    {
        public static string AdoptionNoticeUserIdEmptyMessage = "User cannot be blank";

        public static string AdoptionNoticeDescriptionLengthMessage = "Description cannot be less than 100 character and greater than 500 character";

        public static string AdoptionNoticeEmptyDescriptionMessage = "Description cannot be blank";

        public static string AdoptionNoticeEmptyLocationIdMessage = "Location cannot be blank";


    }
}
