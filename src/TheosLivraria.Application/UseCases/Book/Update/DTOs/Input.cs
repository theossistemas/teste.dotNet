using FluentValidation;

namespace TheosLivraria.Application.UseCases.Book.Create.DTOs
{
    public class UpdateBookInput
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
    }

    public class UpdateBookInputValidator : AbstractValidator<UpdateBookInput>
    {
        public UpdateBookInputValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("O Id do livro é obrigatório.")
               .Must(id => id != Guid.Empty).WithMessage("O Id do livro deve ser um GUID válido.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("O título do livro é obrigatório.")
                .MaximumLength(200).WithMessage("O título do livro deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("O autor é obrigatório.")
                .MaximumLength(150).WithMessage("O nome do autor deve ter no máximo 150 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("A descrição é obrigatória.")
                .MaximumLength(600).WithMessage("A descrição deve ter no máximo 600 caracteres.");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("A quantidade em estoque não pode ser negativa.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

            RuleFor(x => x.PublishedDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data de publicação não pode ser futura.");
        }
    }
}
