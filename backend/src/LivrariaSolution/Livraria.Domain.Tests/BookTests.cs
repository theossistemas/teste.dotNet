// Arquivo: Livraria.Domain.Tests/BookTests.cs

using System;
using FluentAssertions;
using Livraria.Domain.Entities;
using Livraria.Domain.ExcMsg;
using Xunit;

namespace Livraria.Domain.Tests
{
    public class BookTests
    {
        // Setup: Simulação das mensagens de erro para os testes
        // Ajuste estas strings para corresponder ao seu arquivo .resx
        public BookTests() { }

        #region Create Method Tests

        [Fact]
        public void Create_WithValidData_ShouldCreateBookSuccessfully()
        {
            // Arrange
            string title = "1984";
            string author = "George Orwell";
            int publicationYear = 1949;

            // Act
            var book = Book.Create(title, author, publicationYear);

            // Assert
            book.Should().NotBeNull();
            book.Id.Should().NotBeEmpty();
            book.Title.Should().Be(title);
            book.Author.Should().Be(author);
            book.PublicationYear.Should().Be(publicationYear);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Create_WithInvalidTitle_ShouldThrowInvalidOperationException(string invalidTitle)
        {
            // Arrange
            Action act = () => Book.Create(invalidTitle, "George Orwell", 1949);
            var expectedMessage = string.Format(DomainExcMsg.EXC0001, "título");

            // Act & Assert
            act.Should().Throw<InvalidOperationException>()
               .WithMessage(expectedMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Create_WithInvalidAuthor_ShouldThrowInvalidOperationException(string invalidAuthor)
        {
            // Arrange
            Action act = () => Book.Create("1984", invalidAuthor, 1949);
            var expectedMessage = string.Format(DomainExcMsg.EXC0001, "autor(a)");

            // Act & Assert
            act.Should().Throw<InvalidOperationException>()
               .WithMessage(expectedMessage);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Create_WithPastInvalidPublicationYear_ShouldThrowInvalidOperationException(int invalidYear)
        {
            // Arrange
            Action act = () => Book.Create("1984", "George Orwell", invalidYear);

            // Act & Assert
            act.Should().Throw<InvalidOperationException>()
               .WithMessage(DomainExcMsg.EXC0002);
        }

        [Fact]
        public void Create_WithFuturePublicationYear_ShouldThrowInvalidOperationException()
        {
            // Arrange
            int futureYear = DateTime.Now.Year + 1;
            Action act = () => Book.Create("Livro do Futuro", "Autor Viajante", futureYear);

            // Act & Assert
            act.Should().Throw<InvalidOperationException>()
               .WithMessage(DomainExcMsg.EXC0002);
        }

        #endregion

        #region Update Method Tests

        [Fact]
        public void Update_WithValidData_ShouldUpdateBookProperties()
        {
            // Arrange
            var book = Book.Create("Título Antigo", "Autor Antigo", 2000);
            string newTitle = "Título Novo";
            string newAuthor = "Autor Novo";
            int newYear = 2024;

            // Act
            book.Update(newTitle, newAuthor, newYear);

            // Assert
            book.Title.Should().Be(newTitle);
            book.Author.Should().Be(newAuthor);
            book.PublicationYear.Should().Be(newYear);
        }

        [Fact]
        public void Update_WithInvalidTitle_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var book = Book.Create("Título Válido", "Autor Válido", 2020);
            Action act = () => book.Update("", "Autor Válido", 2020);
            var expectedMessage = string.Format(DomainExcMsg.EXC0001, "título");

            // Act & Assert
            act.Should().Throw<InvalidOperationException>()
               .WithMessage(expectedMessage);
        }

        #endregion
    }
}