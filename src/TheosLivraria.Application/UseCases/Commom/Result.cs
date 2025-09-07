namespace TheosLivraria.Application.UseCases.Commom
{
    public class Result<T>
    {
        public bool Success { get; }
        public string? ErrorMessage { get; }
        public object? MetaData { get; }
        public T? Value { get; }

        private Result(bool success, T? value, string? errorMessage, object? metaData = null)
        {
            Success = success;
            Value = value;
            ErrorMessage = errorMessage;
            MetaData = metaData;
        }

        public static Result<T> Ok(T value) => new Result<T>(true, value, null);

        public static Result<T> Fail(string errorMessage, object? metaData = null)
            => new Result<T>(false, default, errorMessage, metaData);
    }

}

