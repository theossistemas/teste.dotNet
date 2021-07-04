using Livraria.Domain.Entities.Administracao;
using System.Threading.Tasks;

namespace Livraria.Infra.Data.Interfaces.Repositories.Administracao
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario> Autenticar(string email, string senha);

        Task<Usuario> LoadByEmail(string email);
    }
}
