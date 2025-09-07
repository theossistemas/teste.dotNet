using FluentValidation;

namespace TheosLivraria.Application.UseCases.User.Auth.DTOs
{
    public class AuthUserInputValidator : AbstractValidator<AuthUserInput>
    {
        public AuthUserInputValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("E-mail inválido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.");
        }
    }
}
