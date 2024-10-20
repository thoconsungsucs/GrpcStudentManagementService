using System.Runtime.Serialization;

namespace GrpcStudentManagementService.Exceptions
{
    [DataContract]
    public class Result
    {
        [DataMember(Order = 1)]
        public bool IsSuccess { get; set; }

        [DataMember(Order = 2)]
        public string? Error { get; set; }

        public Result() { }

        public Result(bool isSuccess, string error)
        {
            if (isSuccess && !string.IsNullOrEmpty(error) || !isSuccess && string.IsNullOrEmpty(error))
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true, string.Empty);
        public static implicit operator Result(string error) => new Result(false, error);

    }

    [DataContract]
    public class Result<TValue>
    {
        [DataMember(Order = 1)]
        public bool IsSuccess { get; set; }

        [DataMember(Order = 2)]
        public string? Error { get; set; }
        [DataMember(Order = 3)]
        private readonly TValue? _value;
        public Result() { }

        public Result(TValue? value, bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = "error";
            _value = value;
        }

        public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException("There is no value for failure result");

        public static implicit operator Result<TValue>(TValue value)
        {
            var res = new Result<TValue>(value, true, string.Empty);

            return new Result<TValue>(value, true, string.Empty);
        }
        public static implicit operator Result<TValue>(string error) => new Result<TValue>(default, false, error);
    }
}
