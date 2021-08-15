using LivrariaTheos.Core.DomainObjects;

namespace LivrariaTheos.Estoque.Domain.Logs
{
    public class LogAplicacao : Entity<int, LogAplicacao>, IAggregateRoot
    {
        public string Metodo { get; private set; }
        public string MensagemErro { get; private set; }
        public string StackTrace { get; private set; }

        public LogAplicacao(string metodo, string mensagemErro, string stackTrace)
        {
            Metodo = metodo;
            MensagemErro = mensagemErro;
            StackTrace = stackTrace;
        }

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Metodo, "O campo Metodo do logAplicacao não pode estar vazio");
        }
    }
}