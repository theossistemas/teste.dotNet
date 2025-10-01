using FluentValidation;
using Library.Shared.Messages;
using Library.Shared.Validators;

namespace Library.Application.MediatR.Books.Commands.Delete;

public class BookDeleteValidator : AbstractValidator<BookDeleteCommand>
{
    public BookDeleteValidator()
    {
        RuleFor(x => x.Id)
                .Must(StringValidators.IsGuid)
                .WithMessage(FluentValidationMessages.INVALID_VALUE);
    }
}