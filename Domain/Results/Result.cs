using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Results
{
    public class Result
    {
        public bool IsSuccess { get; }
        public List<string> Errors { get; } = new();

        private Result(bool isSuccess, List<string>? errors = null)
        {
            IsSuccess = isSuccess;
            if (errors != null)
                Errors = errors;
        }

        public static Result Success() => new(true);
        public static Result Failure(List<string> errors) => new(false, errors);
        public static Result Failure(string error) => new(false, new List<string> { error });

    }
}
