using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Results
{
    public static class ResultFactory
    {
        /// <summary>
        /// Factory methods for creating success or failure results
        /// to keep service code cleaner.
        /// </summary>

        public static Result<T> Success<T>(T data)
             => Result<T>.Success(data);

        public static Result<T> Fail<T>(ErrorType type, IReadOnlyList<string> message)
            => Result<T>.Fail(type, message);
        public static Result<T> Fail<T>(ErrorType type, string message)
        {
            IReadOnlyList<string> messages = new List<string> { message };
            return Result<T>.Fail(type, messages);
        }

        public static Result<T> Fail<T>(Error error)
            => Result<T>.Fail(error);

    }
}
