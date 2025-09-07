public interface ILogService
{
    Task SaveRequestAsync(string path, string method, string requestBody, string responseBody, int statusCode);
    Task SaveExceptionAsync(string path, string method, Exception exception);
}
