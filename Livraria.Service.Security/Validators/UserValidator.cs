using FluentValidation;
using Livraria.Domain.Security.Entities;

namespace Livraria.Service.Security.Validators
{
    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator(){            
            RuleFor(c => c.Login)
                .NotEmpty().WithMessage("Por favor, preencha o login")
                .NotNull().WithMessage("Por favor, preencha o login");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Por favor, preencha o name")
                .NotNull().WithMessage("Por favor, preencha o name");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Por favor, preencha a password")
                .NotNull().WithMessage("Por favor, preencha a password");

            RuleFor(c => c.Role)
                .NotEmpty().WithMessage("Por favor, preencha o role")
                .NotNull().WithMessage("Por favor, preencha o role");
        }
    }
}