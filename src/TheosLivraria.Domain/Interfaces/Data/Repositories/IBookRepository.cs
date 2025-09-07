
using TheosLivraria.Domain.Interfaces.Repositories;
using TheosLivraria.Domain.Models.Entities;
using TheosLivraria.Domain.Models.Filters.Book;

namespace TheosLivraria.Domain.Interfaces.Repository
{
    public interface IBookRepository : IBaseRepository<BookEntity>
    {
        Task<(List<BookEntity> books, int count)> ListBooksAsync(ListBookFilter filter);
        Task<BookEntity?> GetByTitleAsync(string title);
    }
}
