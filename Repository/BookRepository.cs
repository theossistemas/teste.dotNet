using Architecture;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class BookRepository : RepositoryBase<Book, DbContext>, IBookRepository
    {
        public BookRepository(ApplicationDataContext dbContext) : base(dbContext)
        {
        }
    }
}
