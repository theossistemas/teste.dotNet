using Livraria.Domain.Entities;

namespace Livraria.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(Guid id);
        Task<Book?> GetByTitleAsync(string title);
        Task<IEnumerable<Book>> GetAllSortedByNameAsync();
        Task AddAsync(Book book);
        void Update(Book book);
        void Delete(Book book);
        Task<int> SaveChangesAsync();
    }
}