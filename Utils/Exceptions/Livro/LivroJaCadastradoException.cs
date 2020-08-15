using System;
using System.Runtime.Serialization;

namespace Utils.Exceptions.Livro
{
    public class LivroJaCadastradoException : Exception
    {
        public LivroJaCadastradoException()
        {
        }

        public LivroJaCadastradoException(string message) : base(message)
        {
        }

        public LivroJaCadastradoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LivroJaCadastradoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
