using Livraria.Application.DTOs;

namespace Livraria.Application.Services
{
    public interface IBookService
    {
        Task<Guid> CreateBookAsync(CreateBookDTO createBookDto);
        Task UpdateBookAsync(Guid id, UpdateBookDTO updateBookDto);
        Task DeleteBookAsync(Guid id);
        Task<BookDTO?> GetBookByIdAsync(Guid id);
        Task<IEnumerable<BookDTO>> GetAllBooksAsync();
    }
}
