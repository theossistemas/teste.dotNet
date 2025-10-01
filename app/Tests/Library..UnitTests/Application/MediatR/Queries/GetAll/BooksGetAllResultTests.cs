using FluentAssertions;
using Library.Application.MediatR.Books.Queries.GetAll;
using Library.Domain.Entities;

namespace Library.UnitTests.Application.MediatR.Queries.GetAll;

public class BooksGetAllResultTests
{
    [Fact]
    public void Constructor_ShouldInitializePropertiesWithDefaults()
    {
        // Arrange & Act
        var result = new BooksGetAllResult();

        // Assert
        result.Id.Should().Be(Guid.Empty);
        result.Name.Should().BeNullOrEmpty();
        result.Author.Should().BeNullOrEmpty();
        result.Summary.Should().BeNullOrEmpty();
        result.Active.Should().BeFalse();
        result.CreatedAt.Should().Be(default(DateTime));
    }

    [Fact]
    public void ImplicitOperator_ShouldMapBookToBooksGetAllResult()
    {
        // Arrange
        var book = Book.Create("Clean Code", "Robert C. Martin", "Guia para escrever código limpo");

        // Act
        BooksGetAllResult result = book; // conversão implícita

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(book.Id);
        result.Name.Should().Be(book.Name);
        result.Author.Should().Be(book.Author);
        result.Summary.Should().Be(book.Summary);
        result.Active.Should().Be(book.Active);
        result.CreatedAt.Should().Be(book.CreatedAt);
    }
}