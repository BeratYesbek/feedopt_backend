using Core.Utilities.Result.Abstracts;

namespace Core.Utilities.Result.Concretes
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(bool success, T data, string message) : base(success, message)
        {
            Data = data;
        }

        public DataResult(bool success, T data) : base(success)
        {
            Data = data;
        }

   
        public T Data { get; }
    }
}