using Livraria.Data.EFCore.Context;
using Livraria.Domain.Entity;
using Livraria.Domain.Interface.Repositories;
using System.Linq;

namespace Livraria.Data.EFCore.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(LivrariaContext context) : base(context)
        {
        }

        public Usuario Autenticar(string login, string senha)
        {
            return Set.Where(x => x.Login == login && x.Senha == senha).FirstOrDefault();
        }
    }
}
