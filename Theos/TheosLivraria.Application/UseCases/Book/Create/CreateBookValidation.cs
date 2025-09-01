
using FluentValidation;
using TheosLivraria.Application.UseCases.ViewModels.Request.Book.CreateBook;

namespace TheosLivraria.Application.UseCases.Book.Create
{
    public class CreateBookValidation : AbstractValidator<RequestCreateBookViewModel>
    {
        public CreateBookValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do livro é obrigatório.")
                .MaximumLength(200).WithMessage("O nome do livro deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("O autor do livro é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do autor deve ter no máximo 100 caracteres.");
        }
    }
}
