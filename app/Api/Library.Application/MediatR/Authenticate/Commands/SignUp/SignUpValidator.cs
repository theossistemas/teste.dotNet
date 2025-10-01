using FluentValidation;
using Library.Shared.Messages;

namespace Library.Application.MediatR.Authenticate.Commands.SignUp;

public class SignUpValidator : AbstractValidator<SignUpCommand>
{
    public SignUpValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(FluentValidationMessages.NOT_EMPTY);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(FluentValidationMessages.NOT_EMPTY);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(FluentValidationMessages.NOT_EMPTY);
    }
}