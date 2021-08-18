using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TesteTheos.Data.QueryProviders
{
    public class LivrosQueryProvider
    {
        public readonly IQueryable<Livro> BaseQuery;
        public DataContext DataContext { get; init; }

        public LivrosQueryProvider(DataContext dataContext)
        {
            DataContext = dataContext;
            BaseQuery = dataContext.Livros;
        }

        public Task ThrowIfNotExistsAsync(Guid id, CancellationToken cancellationToken)
            => ThrowIfNotExistsAsync(GetByIdQuery(id), cancellationToken);

        public async Task ThrowIfNotExistsAsync(IQueryable<Livro> query, CancellationToken cancellationToken)
        {
            if (!(await query.AnyAsync(cancellationToken)))
            {
                throw new ModelNotFoundException($"Livro não encontrado");
            }
        }

        private IQueryable<Livro> GetAlreadyExistsQuery(string nome, string autor)
            => BaseQuery.Where(q => q.Nome == nome && q.Autor == autor);

        private async Task ThrowIfAlreadyExistsAsync(IQueryable<Livro> query, CancellationToken cancellationToken)
        {
            if (await query.AnyAsync(cancellationToken))
            {
                throw new BadRequestException("Livro já cadastrado");
            }
        }

        public Task ThrowIfAlreadyExistsAsync(Guid id, string nome, string autor, CancellationToken cancellationToken)
            => ThrowIfAlreadyExistsAsync(GetAlreadyExistsQuery(nome, autor).Where(q => q.Id != id), cancellationToken);

        public Task ThrowIfAlreadyExistsAsync(string nome, string autor, CancellationToken cancellationToken)
            => ThrowIfAlreadyExistsAsync(GetAlreadyExistsQuery(nome, autor), cancellationToken);

        public IQueryable<Livro> GetByIdQuery(Guid id)
            => BaseQuery.Where(q => q.Id == id);
    }
}
