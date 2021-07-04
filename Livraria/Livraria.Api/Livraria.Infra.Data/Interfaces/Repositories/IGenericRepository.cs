using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Infra.Data.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(int id);
        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(int id);
        Task Delete(TEntity entity);
    }
}
