using livraria.Domain.entities;
using livraria.Domain.interfaces.common;
using livraria.Service.common;
using livraria.Service.interfaces;

namespace livraria.Service
{
    public class PerfilService : Service<Perfil>, IPerfilService
    {
        public PerfilService(IRepository<Perfil> repository) : base(repository)
        {
        }
    }
}
