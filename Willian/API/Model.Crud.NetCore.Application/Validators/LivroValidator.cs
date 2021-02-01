using FluentValidation; 
using Theos.Livraria.Domain.Model.Livro;

namespace Theos.Livraria.Application.Validators
{
    public class LivroValidator : AbstractValidator<RequestLivro>
    {
        public LivroValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty()
                    .WithMessage($"{nameof(RequestLivro.Titulo)} precisa ser preenchido.");

            RuleFor(x => x.Autor)
              .NotEmpty()
                  .WithMessage($"{nameof(RequestLivro.Autor)} precisa ser preenchido.");

            RuleFor(x => x.IdUsuario)
                .ExclusiveBetween(0, int.MaxValue)
                    .WithMessage($"{nameof(RequestLivro.IdUsuario)}  deve ser maior que zero.");
        }
    }
}
