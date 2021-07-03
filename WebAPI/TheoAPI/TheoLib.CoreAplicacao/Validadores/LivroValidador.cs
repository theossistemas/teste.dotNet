using FluentValidation;
using TheoLib.Dominio.Modelo.LivroModelo;

namespace TheoLib.CoreAplicacao.Validadores
{
    public class LivroValidador : AbstractValidator<RequisicaoLivro>
    {
        public LivroValidador()
        {
             RuleFor(x => x.Titulo).NotEmpty().WithMessage($"{nameof(RequisicaoLivro.Titulo)} obrigatório seu preenchimento");

            RuleFor(x => x.Autor).NotEmpty().WithMessage($"O {nameof(RequisicaoLivro.Autor)} é orbigatório ser informado");

            RuleFor(x => x.IdUsuario).ExclusiveBetween(0, int.MaxValue).WithMessage($"{nameof(RequisicaoLivro.IdUsuario)}  não pode ser ZERO.");
        }
    }
}
