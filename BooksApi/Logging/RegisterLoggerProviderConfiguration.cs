using Microsoft.Extensions.Logging;

namespace BooksApi.Logging
{
    public class RegisterLoggerProviderConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public int EventId { get; set; }
    }
}