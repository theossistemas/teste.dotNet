using FluentValidation;
using Livraria.Domain.Entities;

namespace Livraria.Service.Validators
{
    public class LivroValidator : AbstractValidator<Livro>
    {
        public LivroValidator(){            
            RuleFor(c => c.Autor)
                .NotEmpty().WithMessage("Por favor, preencha o autor")
                .NotNull().WithMessage("Por favor, preencha o autor");

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Por favor, preencha o nome")
                .NotNull().WithMessage("Por favor, preencha o nome");

            RuleFor(c => c.Editora)
                .NotEmpty().WithMessage("Por favor, preencha a editora")
                .NotNull().WithMessage("Por favor, preencha a editora");

            RuleFor(c => c.Genero)
                .NotEmpty().WithMessage("Por favor, preencha o genero")
                .NotNull().WithMessage("Por favor, preencha o genero");
        }
    }
}