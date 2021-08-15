using LivrariaTheos.Core.Data;

namespace LivrariaTheos.Estoque.Domain.Logs.Interfaces
{
    public interface ILogAplicacaoRepository : IRepository<LogAplicacao>
    {
        void Adicionar(LogAplicacao logAplicacao);       
    }
}