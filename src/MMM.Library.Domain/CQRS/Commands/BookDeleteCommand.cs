using FluentValidation;
using MMM.IStore.Core.Messages;
using System;

namespace MMM.Library.Domain.CQRS.Commands
{
    public class BookDeleteCommand : Command
    {
        public Guid Id { get; private set; }

        public BookDeleteCommand(Guid id)
        {
            Id = id;
            AggregateId = Id;
        }

        public override bool IsValid()
        {
            ValidationResult = new BookDeleteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class BookDeleteCommandValidation : AbstractValidator<BookDeleteCommand>
    {
        public BookDeleteCommandValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id Inválido");
        }
    }

}
