using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GrpcStudentManagementService.Exceptions
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Error { get; }

        public Result(bool isSuccess, string error)
        {
            if (isSuccess && error != string.Empty || !isSuccess && error == string.Empty)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }
            IsSuccess = isSuccess;
            Error = error;


        }
        public static Result Success() => new Result(true, string.Empty);
        public static Result Failure(string error) => new Result(false, error);

        public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value, true, string.Empty);
        public static Result<TValue> Failure<TValue>(string error) => new Result<TValue>(default, false, error);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue? _value;
        public Result(TValue? value, bool isSuccess, string error) : base(isSuccess, error)
        {
            _value = value;
        }

        public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException("There is no value for failure result");
        public static implicit operator Result<TValue>(TValue value) => Success(value);
        public static implicit operator Result<TValue>(string error) => Failure<TValue>(error);
    }
}
