using livraria.Context;
using livraria.Domain.entities;
using livraria.Domain.interfaces;
using livraria.Repository.common;

namespace livraria.Repository
{
    public class PerfilRepository : Repository<Perfil>, IPerfilRepository
    {
        public PerfilRepository(LivrariaContext context) : base(context)
        {
        }
    }
}
