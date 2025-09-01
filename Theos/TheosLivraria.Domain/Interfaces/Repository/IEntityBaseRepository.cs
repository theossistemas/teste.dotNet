
using System.Linq.Expressions;

namespace TheosLivraria.Domain.Interfaces.Repository
{
    public interface IEntityBaseRepository<T> : IAsyncDisposable, IDisposable where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null);
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
