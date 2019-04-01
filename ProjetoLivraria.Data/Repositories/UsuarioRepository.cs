using ProjetoLivraria.Domain.Entities;
using ProjetoLivraria.Domain.Repositories.Interfaces;
using System.Linq;

namespace ProjetoLivraria.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ProjetoLivrariaContext context)
            : base(context) { }


        public Usuario Authenticate(string username, string password)
        {
            var usuario = GetAll().FirstOrDefault(u => u.Username == username && u.Password == password);
            return usuario;
        }
    }
}
