using FluentAssertions;
using Livraria.Application.DTOs;
using Livraria.Application.Services;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace Livraria.Application.Tests
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<ILogger<BookService>> _loggerMock;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _loggerMock = new Mock<ILogger<BookService>>();
            _bookService = new BookService(_bookRepositoryMock.Object, _loggerMock.Object);
        }

        #region CreateBookAsync Tests

        [Fact]
        public async Task CreateBookAsync_WithUniqueBook_ShouldCreateAndReturnBookId()
        {
            // Arrange
            var createDto = new CreateBookDTO() { Title = "O Alquimista", Author = "Paulo Coelho", PublicationYear = 1988 };

            _bookRepositoryMock
                .Setup(r => r.GetByTitleAsync(createDto.Title))
                .ReturnsAsync((Book)null);

            // Act
            var resultId = await _bookService.CreateBookAsync(createDto);

            // Assert
            resultId.Should().NotBeEmpty();

            _bookRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Once);
            _bookRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateBookAsync_WhenBookAlreadyExists_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var createDto = new CreateBookDTO() { Title = "Dom Casmurro", Author = "Machado de Assis", PublicationYear = 1899 };

            var existingBook = Book.Create(createDto.Title, createDto.Author, createDto.PublicationYear);
            _bookRepositoryMock
                .Setup(r => r.GetByTitleAsync(createDto.Title))
                .ReturnsAsync(existingBook);

            // Act
            Func<Task> act = () => _bookService.CreateBookAsync(createDto);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                     .WithMessage("Um livro inserido já existe na base de dados.");

            _bookRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Never);
            _bookRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        #endregion

        #region UpdateBookAsync Tests

        [Fact]
        public async Task UpdateBookAsync_WhenBookExists_ShouldUpdateBook()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var updateDto = new UpdateBookDTO() { Title = "Novo Título", Author = "Novo Autor", PublicationYear = 2025 };
            var existingBook = Book.Create("Título Antigo", "Autor Antigo", 2000);

            _bookRepositoryMock.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync(existingBook);

            // Act
            await _bookService.UpdateBookAsync(bookId, updateDto);

            // Assert
            _bookRepositoryMock.Verify(r => r.Update(It.IsAny<Book>()), Times.Once);
            _bookRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);

            existingBook.Title.Should().Be(updateDto.Title);
            existingBook.Author.Should().Be(updateDto.Author);
        }

        [Fact]
        public async Task UpdateBookAsync_WhenBookNotFound_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var updateDto = new UpdateBookDTO() { Title = "Novo Título", Author = "Novo Autor", PublicationYear = 2025 };

            _bookRepositoryMock.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync((Book)null);

            // Act
            Func<Task> act = () => _bookService.UpdateBookAsync(bookId, updateDto);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                     .WithMessage("Livro não encontrado.");
        }

        #endregion

        #region DeleteBookAsync Tests

        [Fact]
        public async Task DeleteBookAsync_WhenBookExists_ShouldDeleteBook()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var existingBook = Book.Create("Livro a ser Deletado", "Autor", 2010);

            _bookRepositoryMock.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync(existingBook);

            // Act
            await _bookService.DeleteBookAsync(bookId);

            // Assert
            _bookRepositoryMock.Verify(r => r.Delete(existingBook), Times.Once);
            _bookRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteBookAsync_WhenBookNotFound_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            _bookRepositoryMock.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync((Book)null);

            // Act
            Func<Task> act = () => _bookService.DeleteBookAsync(bookId);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                     .WithMessage("Livro não encontrado.");
        }

        #endregion

        #region GetBookByIdAsync Tests

        [Fact]
        public async Task GetBookByIdAsync_WhenBookExists_ShouldReturnBookDTO()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var book = Book.Create("O Hobbit", "J.R.R. Tolkien", 1937);

            _bookRepositoryMock.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync(book);

            // Act
            var result = await _bookService.GetBookByIdAsync(bookId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BookDTO>();
            result?.Title.Should().Be(book.Title);
        }

        [Fact]
        public async Task GetBookByIdAsync_WhenBookNotFound_ShouldReturnNull()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            _bookRepositoryMock.Setup(r => r.GetByIdAsync(bookId)).ReturnsAsync((Book)null);

            // Act
            var result = await _bookService.GetBookByIdAsync(bookId);

            // Assert
            result.Should().BeNull();
        }

        #endregion

        #region GetAllBooksAsync Tests

        [Fact]
        public async Task GetAllBooksAsync_WhenBooksExist_ShouldReturnListOfBookDTOs()
        {
            // Arrange
            var books = new List<Book>
            {
                Book.Create("Livro 1", "Autor A", 2001),
                Book.Create("Livro 2", "Autor B", 2002)
            };
            _bookRepositoryMock.Setup(r => r.GetAllSortedByNameAsync()).ReturnsAsync(books);

            // Act
            var result = await _bookService.GetAllBooksAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().Title.Should().Be("Livro 1");
        }

        [Fact]
        public async Task GetAllBooksAsync_WhenNoBooksExist_ShouldReturnEmptyList()
        {
            // Arrange
            _bookRepositoryMock.Setup(r => r.GetAllSortedByNameAsync()).ReturnsAsync(new List<Book>());

            // Act
            var result = await _bookService.GetAllBooksAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        #endregion
    }
}