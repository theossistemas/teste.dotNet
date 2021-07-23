using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Writers;

namespace BooksApi.Logging
{
    public class CustomLogger: ILogger
    {
        private readonly string _loggerName;
        private readonly RegisterLoggerProviderConfiguration _loggerConfiguration;

        public CustomLogger(string name, RegisterLoggerProviderConfiguration config)
        {
            _loggerName = name;
            _loggerConfiguration = config;
        }
        
        
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string mensagem = string.Format("{0}: {1} - {2}", logLevel.ToString(), eventId.Id,
                formatter(state, exception));
            WriteText(mensagem);
        }

        private void WriteText(string mensagem)
        {
            string fileLog = "Log.txt";
            string path = Directory.GetCurrentDirectory() + "\\" + fileLog;
           

            using (StreamWriter streamWriter = new StreamWriter(path,true))
            {
                streamWriter.WriteLine(mensagem);
                streamWriter.Close();
                
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}