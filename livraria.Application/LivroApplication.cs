using livraria.Application.common;
using livraria.Domain.entities;
using livraria.Application.interfaces;
using livraria.Service.interfaces;

namespace livraria.Application
{
    public class LivroApplication : Application<Livro>, ILivroApplication
    {
        public LivroApplication(ILivroService service) : base(service)
        {
        }
    }
}
