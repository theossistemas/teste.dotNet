using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
