using livraria.Application.interfaces.common;
using livraria.Domain.entities;

namespace livraria.Application.interfaces
{
    public interface IUsuarioApplication: IApplication<Usuario>
    {
        Usuario Login(Usuario usuario);
    }
}
