using Core.Utilities.Result.Abstracts;

namespace Core.Utilities.Result.Concretes
{
    public class Result : IResult
    {
        public Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }



        public bool Success { get; }
        public string Message { get; set; }
    }
}