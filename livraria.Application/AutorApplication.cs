using livraria.Domain.entities;
using livraria.Application.common;
using livraria.Application.interfaces;
using livraria.Service.interfaces;

namespace livraria.Application
{
    public class AutorApplication : Application<Autor>, IAutorApplication
    {
        public AutorApplication(IAutorService service) : base(service)
        {
        }
    }
}
