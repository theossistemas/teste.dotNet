using Livraria.Domain.Entities.Administracao;
using System.Threading.Tasks;

namespace Livraria.Infra.Data.Interfaces.Repositories.Administracao
{
    public interface ILogRepositorio : IGenericRepository<Log>
    {
        Task DeleteAll();
    }
}
