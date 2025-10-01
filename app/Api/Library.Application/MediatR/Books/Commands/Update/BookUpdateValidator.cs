using FluentValidation;
using Library.Shared.Messages;
using Library.Shared.Validators;

namespace Library.Application.MediatR.Books.Commands.Update;

public class BookUpdateValidator : AbstractValidator<BookUpdateCommand>
{
    public BookUpdateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(FluentValidationMessages.NOT_EMPTY);

        RuleFor(x => x.Name)
            .MaximumLength(200)
            .WithMessage(FluentValidationMessages.MAX_LENGTH.Replace("{0}", "200"));

        RuleFor(x => x.Author)
            .NotEmpty()
            .WithMessage(FluentValidationMessages.NOT_EMPTY);

        RuleFor(x => x.Name)
             .MaximumLength(150)
             .WithMessage(FluentValidationMessages.MAX_LENGTH.Replace("{0}", "150"));

        RuleFor(x => x.Summary)
            .NotEmpty()
            .WithMessage(FluentValidationMessages.NOT_EMPTY);

        RuleFor(x => x.Summary)
             .MaximumLength(1000)
             .WithMessage(FluentValidationMessages.MAX_LENGTH.Replace("{0}", "1000"));
    }
}