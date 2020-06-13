using FluentAssertions;
using MMM.Library.Domain.CQRS.Commands;
using MMM.Library.Domain.Tests.DataGenerator;
using System;
using Xunit;
using Xunit.Abstractions;

namespace MMM.Library.Domain.Tests.CQRTests
{
    public class BookCommandTests
    {
        readonly ITestOutputHelper _outputHelper;

        public BookCommandTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact(DisplayName = "New Valid Command")]
        [Trait("Category", "Book - Command")]
        public void New_Valid_Command()
        {
            // Arrange
            var book = BookTestsBogusFixture.GetValidBook();
            var command = new BookAddCommand(Guid.NewGuid(), book.Title, book.Year, book.Language, book.Location);

            // Act
            var validate = command.IsValid();

            // Assert
            validate.Should().BeTrue();
            command.ValidationResult.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = "New InValid Command")]
        [Trait("Category", "Book - Command")]
        public void New_InValid_Command()
        {
            // Arrange
            var book = BookTestsBogusFixture.GetInValidBook();
            var command = new BookAddCommand(Guid.NewGuid(), book.Title, book.Year, book.Language, book.Location);

            // Act
            var validate = command.IsValid();

            // Assert
            validate.Should().BeFalse();
            command.ValidationResult.Errors.Should().HaveCountGreaterOrEqualTo(1, "Must Have validations errors");

            _outputHelper.WriteLine($"Where Found {command.ValidationResult.Errors.Count} errors on this validation");

            foreach (var item in command.ValidationResult.Errors)
            {
                _outputHelper.WriteLine($"Error Message::: {item.ErrorMessage}");
            }
        }
    }
}
