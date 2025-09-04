using Microsoft.EntityFrameworkCore;
using TheosLivraria.Core.Queries.Livros;
using TheosLivraria.Core.Util;
using TheosLivraria.Infra.Configuracao;

namespace TheosLivraria.Infra.Repository.Livros.Query
{
    public class ObterLivroPorIdQueryHandler(TheosApplicationDbContext context)
    {
        public async Task<Retorno<LivroDTO>> ExecuteAsync(ObterLivroPorIdQuery query)
        {
            var result = await context.Livros.FirstAsync(x => x.Uuid == query.Id);
            var livroDTO = new LivroDTO(result.Uuid, result.Titulo, result.Autor, result.Isbn, result.DataPublicacao, result.Preco, result.Estoque);

            return Retorno<LivroDTO>.Ok(livroDTO);
        }
    }
}
