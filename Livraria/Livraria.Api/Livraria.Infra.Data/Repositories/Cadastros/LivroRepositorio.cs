using Livraria.Domain.Entities.Cadastros;
using Livraria.Infra.Data.Context;
using Livraria.Infra.Data.Interfaces.Repositories.Cadastros;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Livraria.Infra.Data.Repositories.Cadastros
{
    public class LivroRepositorio : GenericRepository<Livro>, ILivroRepositorio
    {
        public LivroRepositorio(LivrariaDataContext dataContext) : base(dataContext)
        {
        }

        public Task DeleteAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Livro> GetByTituloEGenero(string titulo, int generoId)
        {
            return await GetDataContext().Set<Livro>()
             .AsNoTracking()
             .FirstOrDefaultAsync(e => e.Titulo == titulo && e.GeneroId == generoId);
        }
    }
}
