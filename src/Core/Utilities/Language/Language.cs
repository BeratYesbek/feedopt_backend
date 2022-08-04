

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
