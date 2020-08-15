using System;
using System.Runtime.Serialization;

namespace Utils.Exceptions.Atualizacao
{
    public class AtualizacaoAutomaticaException : Exception
    {
        private const String mensagemErro = "Ocorreu um erro na atualização automática, GUID: {0} , Número: {1}, motivo: {2}";

        public AtualizacaoAutomaticaException()
        {
        }

        public AtualizacaoAutomaticaException(String guid, Int64 numero, String erro) : base(String.Format(mensagemErro, guid, numero, erro))
        {
        }
    }
}
