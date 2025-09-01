
using FluentValidation;
using TheosLivraria.Application.UseCases.ViewModels.Request.Book.Update;

namespace TheosLivraria.Application.UseCases.Book.Update
{
    public class UpdateBookValidation : AbstractValidator<RequestUpdateBookViewModel>
    {
        public UpdateBookValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do livro é obrigatório.")
                .MaximumLength(200).WithMessage("O nome do livro deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("O autor do livro é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do autor deve ter no máximo 100 caracteres.");

            RuleFor(x => x.StatusId)
                .NotEmpty().WithMessage("O status do livro é obrigatório.")
                .InclusiveBetween(1, 3).WithMessage("O status do livro deve ser 1 (Disponível), 2 (Emprestado) ou 3 (Reservado).");
        }
    }
}
