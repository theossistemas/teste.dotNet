using Microsoft.Extensions.Logging;

namespace Livraria.Infrastructure.Logging
{
    public class FileLogger(string categoryName, string filePath) : ILogger
    {
        private readonly string _filePath = filePath;
        private readonly string _categoryName = categoryName;
        private static readonly object _lock = new();

        public IDisposable BeginScope<TState>(TState state) => null!;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            var logRecord = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {_categoryName}: {formatter(state, exception)}";
            if (exception != null)
                logRecord += $" Exception: {exception}";
            lock (_lock)
            {
                File.AppendAllText(_filePath, logRecord + Environment.NewLine);
            }
        }
    }

    public class FileLoggerProvider(string filePath) : ILoggerProvider
    {
        private readonly string _filePath = filePath;

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(categoryName, _filePath);
        }
        public void Dispose() { }
    }
}
