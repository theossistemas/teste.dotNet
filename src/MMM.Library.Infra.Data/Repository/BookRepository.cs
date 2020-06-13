using Microsoft.EntityFrameworkCore;
using MMM.Library.Domain.Interfaces;
using MMM.Library.Domain.Models;
using MMM.Library.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMM.Library.Infra.Data.Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context)
        { }

        public async Task<IEnumerable<Book>> GetAllBooksWithCategoryAndAuthor()
        {
            return await _dbContext.Books.AsNoTracking().Include(p => p.Category)
                .OrderBy(p => p.Title).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBookByCategory(int code)
        {
            return await _dbContext.Books.AsNoTracking().Include(p => p.Category)
                .Where(p => p.Category.Code == code).OrderBy(p => p.Title).ToListAsync();
        }
    }
}
