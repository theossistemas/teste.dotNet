using Livraria.Domain.Dto;
using Livraria.Domain.Entidades;

namespace Livraria.Domain.Interfaces.Validadores
{
    public interface IValidadorDelivro
    {
        void Validar(LivroDto dto);
        void ValidarSeLivroExiste(Livro livro);

        void ValidarLivroEncontrado(Livro livro);
    }
}
