using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Messages
{
    public class AnimalSpeciesValidationMessages
    {
        public static string AnimalSpeciesAnimalCategoryEmptyMessage = "Animal Category cannot be blank";

        public static string AnimalSpeciesKindEmptyMessage = "Animal kind cannot be blank";

        public static string AnimalSpeciesKindLengthMessage =
            "Animal kind cannot be less than 2 and grater than 30 character";
    }
}
