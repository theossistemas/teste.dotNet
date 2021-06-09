using FluentValidation;

namespace TMSA.Livraria.Domain.Models.Validations
{
    public class LivroValidations : AbstractValidator<Livro>
    {
        public LivroValidations()
        {
            RuleFor(c => c.ISBN)
                .NotEmpty().WithMessage("Por favor, preencha o campo ISBN.")
                .Length(13, 13).WithMessage("Por favor, preencha o campo ISBN com 13 caracteres.");

            RuleFor(c => c.Titulo)
                .NotEmpty().WithMessage("Por favor, preencha o campo Título.")
                .Length(2, 80).WithMessage("Por favor, preencha o campo Título entre 2 a 80 caracteres.");

            RuleFor(c => c.Genero)
                .NotEmpty().WithMessage("Por favor, preencha o campo Gênero do lívro")
                .Length(2, 80).WithMessage("Por favor, preencha o campo Gênero entre 2 a 80 caracteres.");

            RuleFor(c => c.QuantidadeDePaginas)
                .NotEmpty().WithMessage("Por favor, preencha o campo quantidade de páginas.");

            RuleFor(c => c.NomeDoAutor)
                .NotEmpty().WithMessage("Por favor, preencha o campo Nome do Autor.")
                .Length(2, 80).WithMessage("Por favor, preencha o Nome do Autor entre 2 a 80 caracteres.");
        }
    }
}
