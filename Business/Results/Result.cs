

namespace Business.Results
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();
    }

    public class ResultFactory
    {
        public static Result<T> Success<T>(T data) => new Result<T>
        {
            IsSuccess = true,
            Data = data
        };

        public static Result<T> Fail<T>(List<string> errors) => new Result<T>
        {
            IsSuccess = false,
            Errors = errors
        };

        public static Result<T> Fail<T>(string error) => new Result<T>
        {
            IsSuccess = false,
            Errors = new List<string> { error }
        };
    }
}
