using FluentValidation;
using TheosBookStore.Stock.Domain.Entities;

namespace TheosBookStore.Stock.Domain.Validations
{
    public class BookValidations : AbstractValidator<Book>
    {
        public BookValidations()
        {
            RuleFor(book => book.Title)
                .MinimumLength(3)
                .WithMessage("Book's title should have 3 characters at least");
            RuleFor(book => book.Edition)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Book's edition should be greather or equal than 1");
            RuleFor(book => book.City)
                .MinimumLength(3)
                .WithMessage("Book's city is invalid");

            RuleForEach(book => book.Authors)
                .SetValidator(new AuthorValidations());
            RuleFor(book => book.ISBN)
                .SetValidator(new ISBNValidations());
            RuleFor(book => book.Publisher)
                .SetValidator(new PublisherValidations());
        }
    }
}
