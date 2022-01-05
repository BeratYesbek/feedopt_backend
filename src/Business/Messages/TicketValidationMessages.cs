using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Messages
{
    public class TicketValidationMessages
    {
        public static string TicketUserIdEmptyMessage = "User cannot be blank";

        public static string TicketDescriptionLengthMessage = "Description cannot be less than 150 character and greater than 600 character";

        public static string TicketEmptyDescriptionMessage = "Description cannot be blank";

        public static string TicketEmptyTitleMessage = "Title cannot be blank";

        public static string TicketTitleLengthMessage = "Title cannot be less than 10 character and greater than 35 character";


    }
}
