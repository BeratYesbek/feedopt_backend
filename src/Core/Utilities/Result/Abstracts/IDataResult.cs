
namespace Core.Utilities.Result.Abstracts
{
    public interface IDataResult<T> : IResult
    {
        public T Data { get;  }
    }
}
