using FluentValidation;
using TheosBookStore.Stock.Domain.Entities;

namespace TheosBookStore.Stock.Domain.Validations
{
    public class BookValidations : AbstractValidator<Book>
    {
        public BookValidations()
        {
            RuleForEach(book => book.Authors)
                .SetValidator(new AuthorValidations());
            RuleFor(book => book.ISBN)
                .SetValidator(new ISBNValidations());
        }
    }
}
