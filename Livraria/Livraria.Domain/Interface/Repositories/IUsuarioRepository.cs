using Livraria.Domain.Entity;

namespace Livraria.Domain.Interface.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario Autenticar(string login, string senha);
    }
}
