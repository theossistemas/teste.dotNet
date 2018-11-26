using LC.Domain;
using LC.Infrastruture.Repositories.Contracts;
using LC.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LC.Infrastruture.Repositories.Implementation
{
    public class BookRepositoy : BaseRepository<Book>, IBookRepositoy
    {
        private readonly DataBaseContext _context;

        public BookRepositoy(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public bool CheckSlugCreated(string slug)
        {
            return _context.Set<Book>().Where(c => c.Slug == slug).Count() > 0;
        }

        public IEnumerable<Book> GetOrderedAscendingByName()
        {
            return _context.Set<Book>().OrderBy(l => l.Name).ToList();
        }
    }
}
