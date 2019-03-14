using livraria.Domain.entities;
using livraria.Domain.interfaces.common;
using livraria.Service.common;
using livraria.Service.interfaces;

namespace livraria.Service
{
    public class CategoriaService : Service<Categoria>, ICategoriaService
    {
        public CategoriaService(IRepository<Categoria> repository) : base(repository)
        {
        }
    }
}
