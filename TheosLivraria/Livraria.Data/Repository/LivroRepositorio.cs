using Livraria.Data.Context;
using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Data.Repository
{
    public class LivroRepositorio : Repository<Livro>, ILivroRepositorio
    {
        private readonly LivrariaContext _livrariaContext;
        private readonly ILogger<LivrariaContext> _logger;
        public LivroRepositorio(LivrariaContext livrariaContext, ILogger<LivrariaContext> logger) :base(livrariaContext, logger)
        {
            _livrariaContext = livrariaContext;
            _logger = logger;
        }

        public async Task<Livro> ObterPorTitulo(string titulo)
        {
            var resultado = await _livrariaContext.Livro
                .Include(x => x.Autor)
                .FirstOrDefaultAsync(x => x.Titulo.ToUpper().Trim() == titulo.ToUpper().Trim());
            return resultado;
        }
        public async Task<Livro> ObterPorId(int id)
        {
            var resultado = await _livrariaContext.Livro
                .Include(x => x.Autor)
                .FirstOrDefaultAsync(x => x.Id == id);
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
