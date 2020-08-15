using System;
using Utils.Logs;

namespace Utils.Exceptions
{
    public static class ExceptionExtensions
    {
        public static Exception GravarLog(this Exception exception)
        {
            Log.GravarLog(exception);

            return exception;
        }
    }
}
