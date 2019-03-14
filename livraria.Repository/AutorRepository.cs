using livraria.Context;
using livraria.Domain.entities;
using livraria.Domain.interfaces;
using livraria.Repository.common;

namespace livraria.Repository
{
    public class AutorRepository : Repository<Autor>, IAutorRepository
    {
        public AutorRepository(LivrariaContext context) : base(context)
        {
        }
    }
}
