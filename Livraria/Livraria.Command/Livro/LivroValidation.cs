using FluentValidation;
using Livraria.Command.CustomValidators;

namespace Livraria.Command
{
    public class LivroValidation<T> : AbstractValidator<T> where T : LivroCommand
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
                .Length(3, 300).WithMessage("O Nome deve ter entre 3 e 300 caracteres.");
        }

        protected void ValidateDescricao()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("A descrição deve ser informada.")
                .MaximumLength(2500).WithMessage("A descrição deve ter no máximo 2500 caracteres.");
        }

        protected void ValidateAutor()
        {
            RuleFor(x => x.Autor)
                .NotNull().WithMessage("O autor deve ser informado.");
        }

        protected void ValidateEditora()
        {
            RuleFor(x => x.Editora)
                .NotNull().WithMessage("A editora deve ser informada.");
        }

        protected void ValidateEdicao()
        {
            RuleFor(x => x.Edicao)
                .GreaterThan(0).WithMessage("A edição deve ser um número maior que zero.");
        }
    }
}
