using Livraria.Common.Implementation;
using Livraria.Common.Interface;
using Livraria.Domain.Interfaces.Removedores;
using Livraria.Domain.Interfaces.Repository;
using Livraria.Domain.Interfaces.Validadores;
using System.Threading.Tasks;

namespace Livraria.Domain.Serviços.Removedores
{
    public class RemovedorDeLivro : IRemovedorDeLivro
    {
        private readonly Notify _notify;
        private readonly ILivroRepositorio _livroRepositorio;
        private readonly IValidadorDelivro _validadorDelivro;

        public RemovedorDeLivro(
            INotify notify,
            ILivroRepositorio livroRepositorio,
            IValidadorDelivro validadorDelivro
            )
        {
            _notify = notify.Invoke();
            _livroRepositorio = livroRepositorio;
            _validadorDelivro = validadorDelivro;
        }
        public async Task Remover(int id)
        {
            var livro = await _livroRepositorio.ObterPorIdAsync(id);
            _validadorDelivro.ValidarLivroEncontrado(livro);

            if (_notify.IsValid())
            {
                _livroRepositorio.Remover(livro);
            }
        }
    }
}
