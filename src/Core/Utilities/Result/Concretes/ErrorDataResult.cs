﻿namespace Core.Utilities.Result.Concretes
{
    public class ErrorDataResult<T> : DataResult<T>
    {

        public ErrorDataResult(T data, string message) : base(false, data, message)
        {
        }

        public ErrorDataResult(T data) : base(false, data)
        {

        }

    }
}
