using Library.Domain.Entities;
using System.Linq.Expressions;

namespace Library.Domain.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    Task<List<Book>> GetBookOrderByName(Expression<Func<Book, bool>> filter = null!,
        bool paginate = true,
        int page = 1,
        int pageSize = 10,
        string orderBy = "desc",
        CancellationToken? cancellation = null);
}