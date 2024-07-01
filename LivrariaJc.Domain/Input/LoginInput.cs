using FluentValidation;
using FluentValidation.Results;

namespace LivrariaJc.Domain.Input
{
    public class LoginInput
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public static ValidationResult Validar(LoginInput input)
        {
            return new LoginDtoValidate().Validate(input);
        }
    }

    public class LoginDtoValidate : AbstractValidator<LoginInput>
    {
        public LoginDtoValidate()
        {
            RuleFor(x => x.UserName).NotEmpty()
                .WithMessage("Usuário obrigatório.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Titulo obrigatório.");
        }
    }
}
