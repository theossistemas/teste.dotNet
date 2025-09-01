
using FluentValidation;
using TheosLivraria.Application.UseCases.ViewModels.Request.User.CreateAdmin;

namespace TheosLivraria.Application.UseCases.User.CreateAdmin
{
    public class CreateAdminValidation : AbstractValidator<RequestCreateAdminViewModel>
    {
        public CreateAdminValidation()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("O nome de usuário é obrigatório.")
                .MaximumLength(100).WithMessage("O nome de usuário deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail deve ser um endereço de e-mail válido.")
                .MaximumLength(100).WithMessage("O e-mail deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.")
                .MaximumLength(50).WithMessage("A senha deve ter no máximo 50 caracteres.");
        }
    }
}
