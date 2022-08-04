using Core.Utilities.Result.Abstracts;


namespace Core.Utilities.Business
{
    public interface IRule
    {
        IResult Run();
    }
}
