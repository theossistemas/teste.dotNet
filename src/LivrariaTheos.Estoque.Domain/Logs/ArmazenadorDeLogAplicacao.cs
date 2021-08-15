using LivrariaTheos.Estoque.Domain.Logs.Interfaces;
using System.Threading.Tasks;

namespace LivrariaTheos.Estoque.Domain.Logs
{
    public class ArmazenadorDeLogAplicacao
    {
        private readonly ILogAplicacaoRepository _logAplicacaoRepositorio;

        public ArmazenadorDeLogAplicacao(ILogAplicacaoRepository logAplicacaoRepositorio)
        {
            _logAplicacaoRepositorio = logAplicacaoRepositorio;

        }

        public async Task Armazenar(LogAplicacao logAplicacao)
        {
            _logAplicacaoRepositorio.Adicionar(logAplicacao);

            await _logAplicacaoRepositorio.UnitOfWork.Commit();            
        }
    }
}