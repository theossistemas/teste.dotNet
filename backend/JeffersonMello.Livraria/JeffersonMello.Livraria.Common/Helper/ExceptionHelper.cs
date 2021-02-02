using Serilog;
using System;

namespace JeffersonMello.Livraria.Common.Helper
{
    public static class ExceptionHelper
    {
        #region Public Methods

        public static string GetMessage(Exception exception)
        {
            string ex = "";

            try
            {
                ex = $"{exception.Message} \n {exception.InnerException}";
                Log.Error(ex + $" \n {exception.StackTrace}");
            }
            catch (Exception)
            {
                //Nothing
            }           

            return ex;
        }

        public static void SetLog(Exception exception)
        {
            var ex = $"{exception.Message} \n {exception.InnerException}";
            Log.Error(ex + $" \n {exception.StackTrace}");
        }

        #endregion Public Methods
    }
}