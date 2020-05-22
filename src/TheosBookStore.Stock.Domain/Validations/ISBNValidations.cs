using FluentValidation;
using TheosBookStore.Stock.Domain.ValueObjects;

namespace TheosBookStore.Stock.Domain.Validations
{
    public class ISBNValidations : AbstractValidator<ISBN>
    {
        private const string ONLY_12DIGITS = "^\\d{12}$";

        public ISBNValidations()
        {
            RuleFor(isbn => isbn.Value)
                .Length(12)
                .Matches(ONLY_12DIGITS)
                .WithMessage("ISBN should have only 12 digits");
        }
    }
}
