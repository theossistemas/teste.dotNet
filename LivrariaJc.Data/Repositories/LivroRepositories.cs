using LivrariaJc.Data.Context;
using LivrariaJc.Data.Repository;
using LivrariaJc.Domain.Entidades;
using LivrariaJc.Domain.Interfaces.Repositories;
using LivrariaJc.Domain.Output;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LivrariaJc.Domain.Input;
using System.Linq;
using LivrariaJc.Domain.Extensions;

namespace LivrariaJc.Data.Repositories
{
    public class LivroRepositories : BaseRepositories<LivrosEntidade>, ILivroRepositories
    {
        private DbSet<LivrosEntidade> _dataset;

        public LivroRepositories(LivrariaJcContext context) : base(context)
        {
            _dataset = context.Set<LivrosEntidade>();
        }

        public async Task<PagedQuery<LivrosEntidade>> ObterPaginadoAsync(LivroFilterInput input)
        {
            return await _dataset
                        .AsNoTracking()
                        .OrderBy(o => o.Titulo)
                        .ToPagedQueryAsync(input.NumeroPagina, input.TamanhoPagina);
        }

        public async Task<bool> VerificaLivroCadastrado(string titulo, int? id = null)
        {
            return await _dataset.AnyAsync(livro => livro.Titulo.ToLower() == titulo.ToLower()
                                                && (id == null || livro.Id != id));
        }
    }
}
