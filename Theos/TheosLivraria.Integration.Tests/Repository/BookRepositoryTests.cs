
using Microsoft.EntityFrameworkCore;
using TheosLivraria.Domain.Models.Entities;
using TheosLivraria.Domain.Models.Filters.Book;
using TheosLivraria.INFRA.Context;
using TheosLivraria.INFRA.Repository;
using Xunit;

namespace TheosLivraria.Integration.Tests.Repository
{
    public class BookRepositoryTests : IDisposable
    {
        private readonly TheosLivrariaDbContext _context;
        private readonly BookRepository _repository;

        public BookRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<TheosLivrariaDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // cada teste com db isolado
                .Options;

            _context = new TheosLivrariaDbContext(options);
            _repository = new BookRepository(_context);

            SeedData().Wait();
        }

        private async Task SeedData()
        {
            var books = new List<BookEntity>
            {
                new BookEntity { Name = "C# Basics", Author = "Author1", Active = true },
                new BookEntity { Name = "EF Core Guide", Author = "Author2", Active = true },
                new BookEntity { Name = "ASP.NET Core", Author = "Author3", Active = false },
                new BookEntity { Name = "Clean Code", Author = "Author4", Active = true },
            };

            await _context.Book.AddRangeAsync(books);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task AddAsync_Should_AddBook()
        {
            var book = new BookEntity { Name = "New Book", Author = "New Author", Active = true };

            await _repository.AddAsync(book);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(book.Id);

            Assert.NotNull(result);
            Assert.Equal("New Book", result!.Name);
        }

        [Fact]
        public async Task Update_Should_UpdateBook()
        {
            var book = await _repository.GetAllAsync(x => x.Active).ContinueWith(t => t.Result.First());
            book.Name = "Updated Name";

            _repository.Update(book);
            await _context.SaveChangesAsync();

            var updatedBook = await _repository.GetByIdAsync(book.Id);
            Assert.Equal("Updated Name", updatedBook!.Name);
        }

        [Fact]
        public async Task GetAllAsync_Should_ReturnActiveBooks()
        {
            var books = await _repository.GetAllAsync(x => x.Active);
            Assert.All(books, b => Assert.True(b.Active));
            Assert.Equal(3, books.ToList().Count); 
        }

        [Fact]
        public async Task GetByIdAsync_Should_ReturnCorrectBook()
        {
            var book = (await _repository.GetAllAsync(x => x.Active)).First();
            var result = await _repository.GetByIdAsync(book.Id);

            Assert.NotNull(result);
            Assert.Equal(book.Id, result!.Id);
        }

        [Fact]
        public async Task GetBooksPagedAsync_Should_ReturnCorrectPageAndCount()
        {
            var filter = new BookAllPagedFilter { Page = 1, Size = 2 };

            var (books, count) = await _repository.GetBooksPagedAsync(filter);

            Assert.Equal(3, count); // total de livros ativos
            Assert.Equal(2, books.Count); // tamanho da página
            Assert.All(books, b => Assert.True(b.Active));
            Assert.Equal("C# Basics", books[0].Name); // ordenação por Name
            Assert.Equal("Clean Code", books[1].Name);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
