using System.Net;

namespace TheosLivraria.Web.Util
{
    public class Retorno<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public Exception? Exception { get; set; }
        public int StatusCode { get; set; }
    }
}
