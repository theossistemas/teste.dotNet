using FluentAssertions;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Messages;
using Moq;
using System.Linq.Expressions;

namespace Library.UnitTests.Domain.Entities;

public class BookTests
{
    [Fact]
    public void Constructor_ShouldSetProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "Clean Code";
        var author = "Robert C. Martin";
        var summary = "Guia de código limpo";
        var createdAt = DateTime.UtcNow;

        // Act
        var book = new Book(id, name, author, summary, true, createdAt);

        // Assert
        book.Id.Should().Be(id);
        book.Name.Should().Be(name);
        book.Author.Should().Be(author);
        book.Summary.Should().Be(summary);
        book.Active.Should().BeTrue();
        book.CreatedAt.Should().Be(createdAt);
    }

    [Fact]
    public void Create_ShouldReturnBookWithCorrectProperties()
    {
        // Act
        var book = Book.Create("DDD", "Eric Evans", "Domain Driven Design");

        // Assert
        book.Name.Should().Be("DDD");
        book.Author.Should().Be("Eric Evans");
        book.Summary.Should().Be("Domain Driven Design");
    }

    [Fact]
    public async Task Update_ShouldThrowException_WhenBookNotFound()
    {
        // Arrange
        var repositoryMock = new Mock<IBookRepository>();
        repositoryMock.Setup(r => r.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync((Book)null!);

        var id = Guid.NewGuid();

        // Act
        Func<Task> act = async () => await Book.Update(id, "Name", "Author", "Summary", repositoryMock.Object, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
                 .WithMessage(string.Format(EntityMessages.EMPTY, "Livro", $"id {id}"));
    }

    [Fact]
    public async Task Update_ShouldCallRepositoryUpdate_WhenBookExists()
    {
        // Arrange
        var existingBook = Book.Create("Old Name", "Old Author", "Old Summary");
        var id = Guid.NewGuid();
        existingBook = new Book(id, existingBook.Name, existingBook.Author, existingBook.Summary, true, DateTime.UtcNow);

        var repositoryMock = new Mock<IBookRepository>();
        repositoryMock.Setup(r => r.GetById(id, It.IsAny<CancellationToken>())).ReturnsAsync(existingBook);
        repositoryMock.Setup(r => r.Update(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync((Book b, CancellationToken _) => b);

        // Act
        var updatedBook = await Book.Update(id, "New Name", "New Author", "New Summary", repositoryMock.Object, CancellationToken.None);

        // Assert
        updatedBook.Name.Should().Be("New Name");
        updatedBook.Author.Should().Be("New Author");
        updatedBook.Summary.Should().Be("New Summary");
        repositoryMock.Verify(r => r.Update(It.IsAny<Book>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Delete_ShouldCallRepositoryDelete()
    {
        // Arrange
        var repositoryMock = new Mock<IBookRepository>();
        var id = Guid.NewGuid();

        // Act
        await Book.Delete(id, repositoryMock.Object, CancellationToken.None);

        // Assert
        repositoryMock.Verify(r => r.Delete(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ExistsBookByName_ShouldThrowException_WhenBookExists()
    {
        // Arrange
        var repositoryMock = new Mock<IBookRepository>();
        repositoryMock.Setup(r => r.Count(It.IsAny< Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        // Act
        Func<Task> act = async () => await Book.ExistsBookByName(repositoryMock.Object, "Clean Code", CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
                 .WithMessage(string.Format(EntityMessages.HAS_VALUE, "Livro", "nome (Clean Code)"));
    }

    [Fact]
    public async Task ExistsBookByName_ShouldNotThrow_WhenBookDoesNotExist()
    {
        // Arrange
        var repositoryMock = new Mock<IBookRepository>();
        repositoryMock.Setup(r => r.Count(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(0);

        // Act
        Func<Task> act = async () => await Book.ExistsBookByName(repositoryMock.Object, "Clean Code", CancellationToken.None);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task ExistsBookByNameUpdate_ShouldNotThrow_WhenNameIsUnique()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var bookId = Guid.NewGuid();
        string name = "Unique Book";

        mockRepo.Setup(r => r.Count(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(0); // nenhum livro com o mesmo nome

        // Act
        Func<Task> act = async () =>
            await Book.ExistsBookByNameUpdate(mockRepo.Object, bookId, name, CancellationToken.None);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task ExistsBookByNameUpdate_ShouldThrow_WhenNameAlreadyExists()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var bookId = Guid.NewGuid();
        string name = "Duplicate Book";

        mockRepo.Setup(r => r.Count(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1); // já existe um livro com esse nome

        // Act
        Func<Task> act = async () =>
            await Book.ExistsBookByNameUpdate(mockRepo.Object, bookId, name, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("*nome (Duplicate Book)*"); // verifica a mensagem parcial
    }
}