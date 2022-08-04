using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Messages.BusinessRulesMessages
{
    internal class AdvertBusinessRulesMessages
    {
        internal const string LessFileSizeMessage = "You must upload at least five file";
        internal const string GreaterFileSizeMessage = "You must upload at most one file";
        internal const string DeletedImagesMessage = "You cannot delete all images";
        internal const string MaxImageSizeMessage = "You cannot upload images more than five";


        internal const string BannedKeywordMessage =
            "You must check your description. It includes  some illegal keywords. they are: ";
    }
}
