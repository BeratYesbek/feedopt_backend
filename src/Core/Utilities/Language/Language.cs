using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Language
{
    public enum PreferredLanguage
    {
        en,
        tr,
        fr,
    }
    public class Language
    {
        public static string[] SupportedLanguage = { PreferredLanguage.en.ToString(), PreferredLanguage.tr.ToString(), PreferredLanguage.fr.ToString() };
    }
}
