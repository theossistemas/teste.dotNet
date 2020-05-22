using FluentValidation;
using TheosBookStore.Stock.Domain.Entities;

namespace TheosBookStore.Stock.Domain.Validations
{
    public class AuthorValidations : AbstractValidator<Author>
    {
        public AuthorValidations()
        {
            RuleFor(author => author.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Author's name is required");
        }
    }
}
