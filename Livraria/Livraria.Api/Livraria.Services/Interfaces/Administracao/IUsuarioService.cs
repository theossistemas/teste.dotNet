using Livraria.Domain.Entities.Administracao;
using System.Threading.Tasks;

namespace Livraria.Services.Interfaces.Administracao
{
    public interface IUsuarioService
    {
        Task<Usuario> Autenticar(string email, string senha);

        Task Registrar(Usuario usuario);
    }
}
