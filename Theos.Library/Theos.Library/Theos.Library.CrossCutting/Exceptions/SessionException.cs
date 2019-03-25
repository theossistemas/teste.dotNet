using System;
using System.Runtime.Serialization;

namespace Theos.Library.CrossCutting.Exceptions
{
    public class SessionException : Exception
    {
        public SessionException()
        {
        }

        public SessionException(string message) : base(message)
        {
        }

        public SessionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SessionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
