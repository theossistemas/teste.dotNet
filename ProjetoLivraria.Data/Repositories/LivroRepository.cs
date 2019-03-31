using ProjetoLivraria.Domain.Entities;
using ProjetoLivraria.Domain.Repositories.Interfaces;
using System.Linq;

namespace ProjetoLivraria.Data.Repositories
{
    public class LivroRepository : Repository<Livro>, ILivroRepository
    {
        public LivroRepository(ProjetoLivrariaContext context)
            : base(context) { }

        public IQueryable<Livro> GetAllOrderByTitle()
        {
            return GetAll().OrderBy(l => l.Titulo);
        }
    }
}
