using System.Linq.Expressions;
using TheosLivraria.Domain.Models.Entities;

namespace TheosLivraria.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
