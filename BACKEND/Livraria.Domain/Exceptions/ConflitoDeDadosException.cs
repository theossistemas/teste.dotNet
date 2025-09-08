using System;

namespace Livraria.Domain.Exceptions
{
    public class ConflitoDeDadosException : Exception
    {
        
        public ConflitoDeDadosException() : base() { }
        public ConflitoDeDadosException(string message) : base(message) { }
    }
}
