using System;

namespace Livraria.Domain.Exceptions
{
    public class OperacaoNaoPermitidaException : Exception
    {
        public OperacaoNaoPermitidaException() : base() { }
        public OperacaoNaoPermitidaException(string message) : base(message) { }
    }
}