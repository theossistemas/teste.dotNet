
using TheosLivraria.Domain.Models.Entities;
using TheosLivraria.Domain.Models.Filters.Book;

namespace TheosLivraria.Domain.Interfaces.Repository
{
    public interface IBookRepository : IEntityBaseRepository<BookEntity>
    {
        Task<(List<BookEntity> books, int count)> GetBooksPagedAsync(BookAllPagedFilter filter);
    }
}
