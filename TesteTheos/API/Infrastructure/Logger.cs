using System;
using System.Diagnostics.CodeAnalysis;
using TesteTheos.Data;

namespace TesteTheos.API
{
    public class Logger : ILogger, IDisposable
    {
        private readonly DataContext dataContext;
        private bool disposedValue;

        public Logger(DataContext dataContext)
            => this.dataContext = dataContext;

        public void Debug(string mensagem)
            => Log("DEBUG", mensagem);

        public void Error(string mensagem)
            => Log("ERROR", mensagem);

        public void Error([NotNull] Exception ex)
        {
            if (ex.InnerException != null)
            {
                Error(ex.InnerException);
                return;
            }

            Log("ERROR", ex.Message, ex.StackTrace);
        }

        public void Info(string mensagem)
            => Log("INFO", mensagem);

        public void Warn(string mensagem)
            => Log("WARN", mensagem);

        private void Log(string level, string mensagem, string stackTrace = null)
        {
            var log = new Log
            {
                Level = level,
                Mensagem = mensagem,
                StackTrace = stackTrace
            };

            dataContext.Logs.Add(log);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                dataContext?.SaveChanges();

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
