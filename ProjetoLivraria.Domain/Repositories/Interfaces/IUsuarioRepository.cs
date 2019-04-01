using ProjetoLivraria.Domain.Entities;

namespace ProjetoLivraria.Domain.Repositories.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario Authenticate(string username, string password);
    }
}
