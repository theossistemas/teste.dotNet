using LibraryStore.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryStore.Core.DataStorage.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(IDbAppContext context)
            : base(context, context.Books)
        { }

        public override async Task<IList<Book>> FindAllAsync()
            => await dbSet.OrderBy(itm => itm.Title).ToListAsync();

        public async Task<bool> ExistsByTitleAsync(string title)
            => await dbSet.AnyAsync(itm => itm.Title.Equals(title));
    }
}