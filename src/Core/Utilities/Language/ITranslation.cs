
namespace Core.Utilities.Language
{
    public interface ITranslation
    {
        string CultureName { get; set; }

        string PropertyName { get; set; }

        string Content { get; set; }
    }
}
