using Livraria.Domain.Dto;
using Livraria.Domain.Entidades;

namespace Livraria.Domain.Interfaces.Alteradores
{
    public interface IAlteradorDeLivro
    {
        void Alterar(Livro livro, LivroDto dto);
    }
}
