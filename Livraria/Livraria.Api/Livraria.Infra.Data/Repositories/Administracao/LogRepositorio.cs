using Livraria.Domain.Entities.Administracao;
using Livraria.Infra.Data.Context;
using Livraria.Infra.Data.Interfaces.Repositories.Administracao;
using System.Threading.Tasks;

namespace Livraria.Infra.Data.Repositories.Administracao
{
    public class LogRepositorio : GenericRepository<Log>, ILogRepositorio
    {
        public LogRepositorio(LivrariaDataContext dataContext) : base(dataContext)
        {
        }

        public Task DeleteAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
