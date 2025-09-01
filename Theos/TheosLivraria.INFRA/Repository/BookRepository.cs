using Microsoft.EntityFrameworkCore;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Models.Entities;
using TheosLivraria.Domain.Models.Filters.Book;
using TheosLivraria.INFRA.Context;

namespace TheosLivraria.INFRA.Repository
{
    public class BookRepository : EntityBaseRepository<BookEntity>, IBookRepository
    {
        private readonly TheosLivrariaDbContext _context;

        public BookRepository(TheosLivrariaDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<(List<BookEntity> books, int count)> GetBooksPagedAsync(BookAllPagedFilter filter)
        {
            IQueryable<BookEntity> query = _context.Book.AsNoTracking();

            query = query.Where(x => x.Active);

            int count = await query.CountAsync();

            query = query.OrderBy(x => x.Name)
                .Skip((filter.Page - 1) * filter.Size)
                .Take(filter.Size);

            return (await query.ToListAsync(), count);

        }
    }

}
