using System.Net;


namespace TheosLivraria.Shared.Util
{
    public class Retorno<T>
    {
        public bool Success { get; }
        public string? Message { get; }
        public T? Data { get; }
        public Exception? Exception { get; }
        public HttpStatusCode StatusCode { get; }

        private Retorno(bool success, T? data, string? message = null, Exception? exception = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Success = success;
            Data = data;
            Message = message;
            Exception = exception;
            StatusCode = statusCode;
        }

        public static Retorno<T> Ok(T data, string? message = null, HttpStatusCode statusCode = HttpStatusCode.OK)
            => new(true, data, message, null, statusCode);

        public static Retorno<T> Falha(string message, Exception? exception = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new(false, default, message, exception, statusCode);
    }
}
