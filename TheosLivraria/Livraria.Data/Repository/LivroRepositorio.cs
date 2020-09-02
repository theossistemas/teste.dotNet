using Livraria.Data.Context;
using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Data.Repository
{
    public class LivroRepositorio : Repository<Livro>, ILivroRepositorio
    {
        private readonly LivrariaContext _livrariaContext;
        public LivroRepositorio(LivrariaContext livrariaContext):base(livrariaContext)
        {
            _livrariaContext = livrariaContext;
        }

        public async Task<Livro> ObterPorTitulo(string titulo)
        {
            var resultado = await _livrariaContext.Livro
                .FirstOrDefaultAsync(x => x.Titulo.ToUpper().Trim() == titulo.ToUpper().Trim());
            return resultado;
        }

        public async Task<List<Livro>> ObterTodosOrdenadoPorNome()
        {
            return await _livrariaContext.Livro
                 .Include(x => x.Autor)
                 .OrderBy(x => x.Titulo)
                 .ToListAsync();
        }
    }
}
