using Persistence.Context;
using Persistence.Entity;
using Persistence.Repository.Interface;

namespace Persistence.Repository
{
    public class LivroRepository : BaseRepository<Livro>, ILivroRepository
    {
        public LivroRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
