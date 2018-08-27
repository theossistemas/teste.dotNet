using Livraria.Data.EFCore.Context;
using Livraria.Domain.Entity;
using Livraria.Domain.Interface.Repositories;

namespace Livraria.Data.EFCore.Repositories
{
    public class LivroRepository : Repository<Livro>, ILivroRepository
    {
        public LivroRepository(LivrariaContext context) : base(context)
        {
        }
    }
}
