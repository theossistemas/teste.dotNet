using Microsoft.EntityFrameworkCore;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Models.Entities;
using TheosLivraria.Domain.Models.Filters.Book;

namespace TheosLivraria.Infrastructure.Data.Repositories
{
    public class BookRepository : BaseRepository<BookEntity>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context) { }

        public async Task<BookEntity?> GetByTitleAsync(string title)
        {
            return await _context.Books.AsNoTracking()
                .FirstOrDefaultAsync(b => b.Title == title && b.IsActive);
        }

        public async Task<(List<BookEntity> books, int count)> ListBooksAsync(ListBookFilter filter)
        {
            IQueryable<BookEntity> query = _context.Books.AsNoTracking();

            query = query.Where(x => x.IsActive);

            int count = await query.CountAsync();

            query = query.OrderBy(x => x.Title)
                .Skip((filter.Page - 1) * filter.Size)
                .Take(filter.Size);

            return (await query.ToListAsync(), count);
        }
    }
}
