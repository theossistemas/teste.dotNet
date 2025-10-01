using System.Linq.Expressions;
using Library.Domain.Entities.Base;

namespace Library.Domain.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T> Insert(T entity, CancellationToken cancellation);
    Task<T> GetById(Guid id, CancellationToken cancellation);
    Task<List<T>> GetAll(
        Expression<Func<T, bool>> filter = null!,
        bool paginate = true,
        int page = 1,
        int pageSize = 10,
        string orderBy = "desc",
        CancellationToken? cancellation = null);
    Task<int> Count(
        Expression<Func<T, bool>> filter = null!, 
        CancellationToken? cancellation = null);
    Task<T> FindOne(
        Expression<Func<T, bool>> filter,
        CancellationToken cancellation);
    Task<T> Update(T entity, CancellationToken cancellation);
    Task Delete(Guid id, CancellationToken cancellation);
}