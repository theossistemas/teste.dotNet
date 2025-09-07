using FluentValidation;

namespace TheosLivraria.Application.UseCases.Book.list.DTOs
{
    public class ListBooksInput
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class ListBooksInputValidator : AbstractValidator<ListBooksInput>
    {
        public ListBooksInputValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("A página deve ser maior que zero.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("O tamanho da página deve ser maior que zero.")
                .LessThanOrEqualTo(100)
                .WithMessage("O tamanho da página não pode ser maior que 100.");
        }
    }
}
