using System;
using System.Runtime.Serialization;

namespace Utils.Exceptions.Usuario
{
    public class LoginOuSenhaInvalidosException : Exception
    {
        public LoginOuSenhaInvalidosException()
        {
        }

        public LoginOuSenhaInvalidosException(string message) : base(message)
        {
        }

        public LoginOuSenhaInvalidosException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LoginOuSenhaInvalidosException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
