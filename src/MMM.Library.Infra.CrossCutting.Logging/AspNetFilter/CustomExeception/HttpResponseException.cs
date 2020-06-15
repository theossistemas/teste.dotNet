using System;

namespace MMM.Library.Infra.CrossCutting.Logging.AspNetFilter.CustomExeception
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;

        public object Value { get; set; }
    }
}
