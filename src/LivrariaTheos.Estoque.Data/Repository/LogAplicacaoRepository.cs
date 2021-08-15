using LivrariaTheos.Core.Data;
using LivrariaTheos.Estoque.Domain.Logs;
using LivrariaTheos.Estoque.Domain.Logs.Interfaces;

namespace LivrariaTheos.Estoque.Data.Repository
{
    public class LogAplicacaoRepository : ILogAplicacaoRepository
    {
        private readonly EstoqueContext _context;

        public LogAplicacaoRepository(EstoqueContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(LogAplicacao logAplicacao)
        {
            _context.LogsAplicacao.Add(logAplicacao);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}