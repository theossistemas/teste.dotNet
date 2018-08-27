using Livraria.Service.DTOs;

namespace Livraria.Service.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioDTO Autenticar(string login, string senha);
    }
}
