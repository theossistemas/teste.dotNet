namespace TheosBookStore.Web.Models
{
    public class ResponseDefault<T>
        where T : class
    {
        public T Data { get; set; }
        public string message { set; get; }
    }
}
