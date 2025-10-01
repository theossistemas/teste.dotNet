using FluentValidation;
using Library.Shared.Messages;

namespace Library.Application.MediatR.Authenticate.Commands.SignIn;

public class SignInValidator : AbstractValidator<SignInCommand>
{
    public SignInValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(FluentValidationMessages.NOT_EMPTY);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(FluentValidationMessages.NOT_EMPTY);
    }
}