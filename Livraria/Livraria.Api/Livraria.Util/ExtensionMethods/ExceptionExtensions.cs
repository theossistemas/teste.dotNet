using System;
using System.Collections.ObjectModel;
using System.Text;

namespace Livraria.Util.ExtensionMethods
{
    public static class ExceptionExtensions
    {
        public static string MontarMensagemErro(this ReadOnlyCollection<Exception> exceptions)
        {
            var mensagens = new StringBuilder();

            foreach (var exception in exceptions)
                mensagens.Append($"{exception.Message}; ");

            return mensagens.ToString();
        }
    }
}
