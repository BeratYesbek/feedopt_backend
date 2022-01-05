using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Messages
{
    public class MissingDeclarationValidationMessages
    {
        public static string MissingDeclarationUserIdEmptyMessage = "User cannot be blank";

        public static string MissingDeclarationDescriptionLengthMessage = "Description cannot be less than 100 character and greater than 500 character";

        public static string MissingDeclarationEmptyDescriptionMessage = "Description cannot be blank";

        public static string MissingDeclarationEmptyLocationIdMessage = "Location cannot be blank";
    }
}
