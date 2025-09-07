using FluentValidation;

namespace TheosLivraria.Application.UseCases.Book.GetById.DTOs
{
    public class GetBookByIdInput
    {
        public Guid Id { get; set; }
    }

    public class GetBookByIdInputValidator : AbstractValidator<GetBookByIdInput>
    {
        public GetBookByIdInputValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O Id do livro é obrigatório.");
        }
    }
}
