using Microsoft.EntityFrameworkCore;
using TheosLivraria.Core.Queries.Livros;
using TheosLivraria.Core.Util;
using TheosLivraria.Infra.Configuracao;

namespace TheosLivraria.Infra.Repository.Livros.Query
{
    public class ObterLivrosQueryHandler(TheosApplicationDbContext context)
    {
        public async Task<Retorno<List<LivroGridDTO>>> ExecuteAsync(ObterLivrosQuery obter)
        {
            var query = context.Livros.AsQueryable().OrderBy(o => o.Titulo);
            return Retorno<List<LivroGridDTO>>.Ok(await query.Select(s => new LivroGridDTO(s.Uuid, s.Titulo, s.Autor, s.Isbn, s.Preco, s.Estoque)).ToListAsync());
        }
    }
}

