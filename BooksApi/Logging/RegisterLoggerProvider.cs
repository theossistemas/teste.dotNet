using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace BooksApi.Logging
{
    public class RegisterLoggerProvider: ILoggerProvider
    {
        private readonly RegisterLoggerProviderConfiguration _loggerConfig;
        readonly ConcurrentDictionary<string, CustomLogger> _loggers = new ConcurrentDictionary<string, CustomLogger>();

        public RegisterLoggerProvider(RegisterLoggerProviderConfiguration config)
        {
            _loggerConfig = config;
        }
        
        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new CustomLogger(name, _loggerConfig));
        }
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

      
    }
}