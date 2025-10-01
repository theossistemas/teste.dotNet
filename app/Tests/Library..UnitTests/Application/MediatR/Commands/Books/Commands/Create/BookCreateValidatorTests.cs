using FluentValidation.TestHelper;
using Library.Application.MediatR.Books.Commands.Create;
using Library.Shared.Messages;

namespace Library.UnitTests.Application.MediatR.Commands.Books.Commands.Create;

public class BookCreateValidatorTests
{
    private readonly BookCreateValidator _validator;

    public BookCreateValidatorTests()
    {
        _validator = new BookCreateValidator();
    }

    [Fact]
    public void Should_HaveError_When_NameIsEmpty()
    {
        var model = new BookCreateCommand { Name = "", Author = "Author", Summary = "Summary" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage(FluentValidationMessages.NOT_EMPTY.Replace("{PropertyName}", "Name"));
    }

    [Fact]
    public void Should_HaveError_When_NameExceedsMaxLength()
    {
        var command = new BookCreateCommand { Name = new string('A', 201), Author = "Author", Summary = "Summary" };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage(string.Format(FluentValidationMessages.MAX_LENGTH.Replace("{PropertyName}", "Name"), 200));
    }

    [Fact]
    public void Should_HaveError_When_AuthorIsEmpty()
    {
        var command = new BookCreateCommand { Name = "Name", Author = "", Summary = "Summary" };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Author)
              .WithErrorMessage(FluentValidationMessages.NOT_EMPTY.Replace("{PropertyName}", "Author"));
    }

    [Fact]
    public void Should_HaveError_When_SummaryIsEmpty()
    {
        var model = new BookCreateCommand { Name = "Name", Author = "Author", Summary = "" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Summary)
              .WithErrorMessage(FluentValidationMessages.NOT_EMPTY.Replace("{PropertyName}", "Summary"));
    }

    [Fact]
    public void Should_HaveError_When_SummaryExceedsMaxLength()
    {
        var command = new BookCreateCommand
        {
            Name = "Name",
            Author = "Author",
            Summary = new string('S', 1001)
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Summary)
              .WithErrorMessage(string.Format(FluentValidationMessages.MAX_LENGTH.Replace("{PropertyName}", "Summary"), 1000));
    }

    [Fact]
    public void Should_NotHaveError_When_AllFieldsAreValid()
    {
        var command = new BookCreateCommand
        {
            Name = "Valid Name",
            Author = "Valid Author",
            Summary = "Valid Summary"
        };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
        result.ShouldNotHaveValidationErrorFor(x => x.Author);
        result.ShouldNotHaveValidationErrorFor(x => x.Summary);
    }
}