using FluentValidation;
using TheosBookStore.Stock.Domain.Entities;

namespace TheosBookStore.Stock.Domain.Validations
{
    public class PublisherValidations : AbstractValidator<Publisher>
    {
        public PublisherValidations()
        {
            RuleFor(publisher => publisher.Name)
                .NotEmpty()
                .WithMessage("Publisher's name is required");
        }
    }
}
