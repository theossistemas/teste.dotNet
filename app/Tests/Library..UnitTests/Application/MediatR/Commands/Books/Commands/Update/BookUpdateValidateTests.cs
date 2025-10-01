using FluentValidation.TestHelper;
using Library.Application.MediatR.Books.Commands.Update;
using Library.Shared.Messages;

namespace Library.UnitTests.Application.MediatR.Commands.Books.Commands.Update;

public class BookUpdateValidateTests
{

    private readonly BookUpdateValidator _validator;

    public BookUpdateValidateTests()
    {
        _validator = new BookUpdateValidator();
    }

    [Fact]
    public void Should_HaveError_When_NameIsEmpty()
    {
        var model = new BookUpdateCommand { Name = "", Author = "Author", Summary = "Summary" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage(FluentValidationMessages.NOT_EMPTY.Replace("{PropertyName}", "Name"));
    }

    [Fact]
    public void Should_HaveError_When_NameExceedsMaxLength()
    {
        var command = new BookUpdateCommand { Name = new string('A', 201), Author = "Author", Summary = "Summary" };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage(string.Format(FluentValidationMessages.MAX_LENGTH.Replace("{PropertyName}", "Name"), 200));
    }

    [Fact]
    public void Should_HaveError_When_AuthorIsEmpty()
    {
        var command = new BookUpdateCommand { Name = "Name", Author = "", Summary = "Summary" };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Author)
              .WithErrorMessage(FluentValidationMessages.NOT_EMPTY.Replace("{PropertyName}", "Author"));
    }

    [Fact]
    public void Should_HaveError_When_SummaryIsEmpty()
    {
        var model = new BookUpdateCommand { Name = "Name", Author = "Author", Summary = "" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Summary)
              .WithErrorMessage(FluentValidationMessages.NOT_EMPTY.Replace("{PropertyName}", "Summary"));
    }

    [Fact]
    public void Should_HaveError_When_SummaryExceedsMaxLength()
    {
        var command = new BookUpdateCommand
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
        var command = new BookUpdateCommand
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