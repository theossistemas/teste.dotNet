using livraria.Domain.entities;
using livraria.Domain.interfaces.common;

namespace livraria.Domain.interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario Login(Usuario usuario);
    }
}
