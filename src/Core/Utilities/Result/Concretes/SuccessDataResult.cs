namespace Core.Utilities.Result.Concretes
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message) : base(true, data, message)
        {
        }

        public SuccessDataResult(T data) : base(true, data)
        {
        }
    }
}
