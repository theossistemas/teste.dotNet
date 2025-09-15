namespace Livraria.Exceptions
{
    public class AutenticacaoJWTException : Exception
    {
        public AutenticacaoJWTException()
            : base($"Usuário não autorizado!") { }
    }
}
