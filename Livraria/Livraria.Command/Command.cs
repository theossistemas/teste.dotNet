using FluentValidation.Results;
using MediatR;

namespace Livraria.Command
{
    public abstract class Command : IRequest
    {
        public ValidationResult ValidationResult { get; set; }

        protected Command() { }

        public abstract bool IsValid();
    }
}
