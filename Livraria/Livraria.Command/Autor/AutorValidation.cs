using FluentValidation;
using Livraria.Command.CustomValidators;

namespace Livraria.Command
{ 
    public class AutorValidation<T> : AbstractValidator<T> where T : AutorCommand
    {
        protected void ValidateId()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O Id deve ser informado.")
                .IsGuid();
        }

        protected void ValidateNome()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O Nome deve ser informado.")
                .Length(3, 200).WithMessage("O Nome deve ter entre 3 e 200 caracteres.");
        }
    }
}
