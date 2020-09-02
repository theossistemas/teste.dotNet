using Livraria.Domain.Dto;
using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Alteradores;

namespace Livraria.Domain.Serviços.Alteradores
{
    public class AlteradorDeLivro : IAlteradorDeLivro
    {
        public void Alterar(Livro livro, LivroDto dto)
        {
            livro.AlterarAnoDePublicao(dto.AnoDePublicacao);
            livro.AlterarAutorId(dto.AutorId);
            livro.AlterarEdicao(dto.Edicao);
            livro.AlterarTitulo(dto.Titulo);
        }
    }
}
