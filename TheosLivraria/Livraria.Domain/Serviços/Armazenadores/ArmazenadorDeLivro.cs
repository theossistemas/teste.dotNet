using Livraria.Common.Implementation;
using Livraria.Common.Interface;
using Livraria.Domain.Dto;
using Livraria.Domain.Interfaces.Armazenadores;
using System.Threading.Tasks;

namespace Livraria.Domain.Serviços.Armazenadores
{
    public class ArmazenadorDeLivro : IArmazenadorDeLivro
    {
        private readonly Notify _notify;

        public ArmazenadorDeLivro(INotify notify)
        {
            _notify = notify.Invoke();
        }
        public Task Armazenar(LivroDto dto)
        {
            if(dto.Edicao > 1)
            {
                _notify.NewNotification("Livro", "Teste de notificações!");
                return null;
            }

            return Task.CompletedTask;
        }
    }
}
