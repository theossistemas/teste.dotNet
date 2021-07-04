using Livraria.Domain.Entities.Administracao;
using Livraria.Infra.Data.Context;
using Livraria.Infra.Data.Interfaces.Repositories.Administracao;
using Livraria.Util.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Livraria.Infra.Data.Repositories.Administracao
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(LivrariaDataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Usuario> Autenticar(string email, string senha)
        {
            return await GetDataContext().Set<Usuario>()
           .AsNoTracking()
           .FirstOrDefaultAsync(e => e.Email == email && e.Senha == senha.HashString());
        }

        public async Task<Usuario> LoadByEmail(string email)
        {
            return await GetDataContext().Set<Usuario>()
                           .AsNoTracking()
                           .FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
