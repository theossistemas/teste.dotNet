using System;

namespace TesteTheos
{
    public interface ILogger
    {
        void Debug(string mensagem);
        void Error(string mensagem);
        void Error(Exception ex);
        void Info(string mensagem);
        void Warn(string mensagem);
    }
}
