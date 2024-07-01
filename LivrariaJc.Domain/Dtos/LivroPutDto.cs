using FluentValidation;
using FluentValidation.Results;

namespace LivrariaJc.Domain.Imput
{
    public class LivroPutDto
    {
        public int Id { get; set; }
        public string Autor { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        public static ValidationResult Validar(LivroPutDto input)
        {
            return new LivroPutDtoValidate().Validate(input);
        }
    }

    public class LivroPutDtoValidate : AbstractValidator<LivroPutDto>
    {
        public LivroPutDtoValidate()
        {
            RuleFor(x => x.Id).NotEmpty()
                .WithMessage("ID obrigatório.");

            RuleFor(x => x.Autor).NotEmpty()
                .WithMessage("Autor obrigatório.")
                .MaximumLength(140)
                .WithMessage("Autor deve ter no máximo 140 caracteres.");

            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("Titulo obrigatório.")
                .MaximumLength(250)
                .WithMessage("Titulo deve ter no máximo 250 caracteres.");

            RuleFor(x => x.Descricao).NotEmpty()
                .WithMessage("Descrição obrigatória.")
                .MaximumLength(1000)
                .WithMessage("Descricao deve ter no máximo 1000 caracteres.");

            RuleFor(x => x.Valor).NotEmpty()
                .WithMessage("Valor obrigatório.");
        }
    }
}
