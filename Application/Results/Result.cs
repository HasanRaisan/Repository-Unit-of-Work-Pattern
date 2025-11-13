using Microsoft.AspNetCore.Mvc;
using Application.Results;

namespace Application.Results
{
    /// <summary>
    /// Represents a result that can either succeed with data
    /// or fail with an error, following the Result Pattern.
    /// </summary>
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Data { get; }
        public Error Error { get; }

        private Result(bool isSuccess, T data, Error error)
        {
            IsSuccess = isSuccess;
            Data = data;
            Error = error;
        }

        /// <summary>
        /// Creates a successful result containing data.
        /// </summary>
        public static Result<T> Success(T data) =>
            new Result<T>(true, data, null);

        /// <summary>
        /// Creates a failed result with the given error.
        /// </summary>
        public static Result<T> Fail(Error error) =>
            new Result<T>(false, default, error);

        /// <summary>
        /// Creates a failed result with the given error type and message.
        /// </summary>
        public static Result<T> Fail(ErrorType type, List<string> message) =>
        new Result<T>(false, default, new Error(type, message));

        //public IActionResult ToActionResult()
        //{
        //   return ResultToActionMapper.ToActionResult(this);
        //}
    }
}
