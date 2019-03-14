using livraria.Context;
using livraria.Domain.entities;
using livraria.Domain.interfaces;
using livraria.Repository.common;

namespace livraria.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(LivrariaContext context) : base(context)
        {
        }
    }
}
