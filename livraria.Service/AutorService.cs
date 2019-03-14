using livraria.Domain.entities;
using livraria.Domain.interfaces.common;
using livraria.Service.common;
using livraria.Service.interfaces;

namespace livraria.Service
{
    public class AutorService : Service<Autor>, IAutorService
    {
        public AutorService(IRepository<Autor> repository) : base(repository)
        {
        }
    }
}
