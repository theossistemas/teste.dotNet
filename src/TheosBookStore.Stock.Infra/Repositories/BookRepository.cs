using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheosBookStore.LibCommon.Repositories;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Domain.Repositories;
using TheosBookStore.Stock.Infra.Context;
using TheosBookStore.Stock.Infra.Models;

namespace TheosBookStore.Stock.Infra.Repositories
{
    public class BookRepository : BaseRepository<Book, BookModel>, IBookRepository
    {
        public BookRepository(TheosBookStoreStockDB dbContext, IMapper mapper)
            : base(dbContext, mapper) { }

        public bool HasAny(Book book)
        {
            return DbSet.Any(table => table.ISBN == book.ISBN);
        }

        protected override BookModel BeforePost(BookModel model, EntityState state)
        {
            Unchange<PublisherModel>(model.Publisher);
            foreach (var bookAuthor in model.Authors)
            {
                Unchange<AuthorModel>(bookAuthor.Author);
            }

            return base.BeforePost(model, state);
        }
    }
}
