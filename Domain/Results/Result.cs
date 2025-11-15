using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Results
{

    public class Result<T>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public T Data { get; }
        public IReadOnlyList<string> Errors { get; }

        private Result(bool isSuccess, T data, IReadOnlyList<string> errors)
        {
            IsSuccess = isSuccess;
            Data = data;
            Errors = errors ?? new List<string>();
        }

        public static Result<T> Success(T data) =>
            new Result<T>(true, data, new List<string>());

        public static Result<T> Fail(string error) =>
            new Result<T>(false, default, new List<string> { error });

        public static Result<T> Fail(IReadOnlyList<string> errors) =>
            new Result<T>(false, default, errors);
    }
}
