using livraria.Domain.entities;
using livraria.Service.interfaces.common;

namespace livraria.Service.interfaces
{
    public interface IUsuarioService : IService<Usuario>
    {
        Usuario Login(Usuario usuario);
    }
}
