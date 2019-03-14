using livraria.Domain.entities;
using livraria.Application.common;
using livraria.Application.interfaces;
using livraria.Service.interfaces;

namespace livraria.Application
{
    public class CategoriaApplication : Application<Categoria>, ICategoriaApplication
    {
        public CategoriaApplication(ICategoriaService service) : base(service)
        {
        }
    }
}
