using Livraria.Common.Implementation;
using Livraria.Common.Interface;
using Livraria.Common.Utils;
using Livraria.Domain.Dto;
using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Validadores;

namespace Livraria.Domain.Serviços.Validadores
{
    public class ValidadorDeLivro : IValidadorDelivro
    {
        private readonly Notify _notify;

        public ValidadorDeLivro(INotify notify)
        {
            _notify = notify.Invoke();
        }

        public void Validar(LivroDto dto)
        {
            if (dto == null)
            {
                _notify.NewNotification(Resources.LivroEntidade, Resources.LivroNulo);
            }
            else if (dto.Autor == null && dto.AutorId < Constantes.Um)
            {
                _notify.NewNotification(Resources.LivroEntidade, Resources.LivroComAutorNulo);
            }
        }

        public void ValidarLivroEncontrado(Livro livro)
        {
            if (livro == null)
                _notify.NewNotification(Resources.LivroEntidade, Resources.LivroNaoEncontrado);
        }

        public void ValidarSeLivroExiste(Livro livro)
        {
            if (livro != null)
                _notify.NewNotification(Resources.LivroEntidade, Resources.LivroJaExiste);
        }
    }
}
