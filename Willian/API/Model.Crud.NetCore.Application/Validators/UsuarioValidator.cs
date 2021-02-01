using FluentValidation; 
using Theos.Livraria.Domain.Model.Usuario;

namespace Theos.Livraria.Application.Validators
{
    public class UsuarioValidator : AbstractValidator<RequestUsuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                    .WithMessage($"{nameof(RequestUsuario.Nome)} precisa ser preenchido.");

            RuleFor(x => x.Email)
              .NotEmpty()
                  .WithMessage($"{nameof(RequestUsuario.Email)} precisa ser preenchido.");
        }
    }
}
