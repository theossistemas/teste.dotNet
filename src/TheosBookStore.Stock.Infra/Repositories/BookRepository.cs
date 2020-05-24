using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using TheosBookStore.LibCommon.Repositories;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Domain.Repositories;
using TheosBookStore.Stock.Domain.ValueObjects;
using TheosBookStore.Stock.Infra.Context;
using TheosBookStore.Stock.Infra.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace TheosBookStore.Stock.Infra.Repositories
{
    public class BookRepository : BaseRepository<Book, BookModel>, IBookRepository
    {
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(TheosBookStoreStockDbContext dbContext, IMapper mapper, ILogger<BookRepository> logger)
            : base(dbContext, mapper)
        {
            _logger = logger;
        }

        public bool HasAny(Book book)
        {
            var shouldFindById = book.Id > 0;
            if (shouldFindById)
                return DbSet.Any(row => row.Id == book.Id);

            return DbSet.Any(row => row.ISBN == book.ISBN);
        }

        public Book GetByISBN(ISBN isbn)
        {
            var bookModel = DbSet
                .Where(book => book.ISBN == isbn.Value)
                .AsNoTracking()
                .Include(b => b.Authors)
                    .ThenInclude(ba => ba.Author)
                .Include(b => b.Publisher)
                .FirstOrDefault();
            var bookEntity = _mapper.Map<Book>(bookModel);
            return bookEntity;
        }

        protected override BookModel BeforePost(BookModel model, EntityState state)
        {
            Unchange<PublisherModel>(model.Publisher);
            foreach (var bookAuthor in model.Authors)
            {
                Unchange<AuthorModel>(bookAuthor.Author);
            }

            LogNewBook(model, state);

            return base.BeforePost(model, state);
        }

        public ICollection<Book> GetOrderedTitleBookList(int take, int offSet)
        {
            var modelList = DbSet
                .AsNoTracking()
                .OrderBy(book => book.Title)
                .Include(b => b.Authors)
                    .ThenInclude(ba => ba.Author)
                .Include(b => b.Publisher)
                .Skip(offSet)
                .Take(take)
                .ToList();
            var entityList = _mapper.Map<ICollection<Book>>(modelList);
            return entityList;
        }

        private void LogNewBook(BookModel model, EntityState state)
        {
            if (state != EntityState.Added)
                return;

            var modelJson = JsonSerializer.Serialize(model);
            _logger.LogInformation($"Adding a new book:{modelJson}");

        }
    }
}
