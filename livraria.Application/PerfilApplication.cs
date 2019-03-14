using livraria.Application.common;
using livraria.Application.interfaces;
using livraria.Domain.entities;
using livraria.Service.interfaces;

namespace livraria.Application
{
    public class PerfilApplication : Application<Perfil>, IPerfilApplication
    {
        public PerfilApplication(IPerfilService service) : base(service)
        {
        }
    }
}
