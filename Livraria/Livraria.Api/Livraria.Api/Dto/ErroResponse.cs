using Livraria.Util.ExtensionMethods;
using System;
using System.Collections.ObjectModel;

namespace Livraria.Api.Dto
{
    public class ErroResponse
    {
        public string Erro { get; private set; }

        public ErroResponse(Exception exception)
        {
            Erro = exception?.Message;
        }

        public ErroResponse(ReadOnlyCollection<Exception> exceptions)
        {
            Erro = exceptions.MontarMensagemErro();
        }
    }
}
