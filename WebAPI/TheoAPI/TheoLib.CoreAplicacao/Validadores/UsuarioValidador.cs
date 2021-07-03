using FluentValidation;
using TheoLib.Dominio.Modelo.UsuarioModelo;

namespace TheoLib.CoreAplicacao.Validadores
{
    public class UsuarioValidador : AbstractValidator<RequisicaoUsuario>
    {
        public UsuarioValidador()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage($"O {nameof(RequisicaoUsuario.Nome)} é obrigatório.");

            RuleFor(x => x.Email).NotEmpty().WithMessage($"O {nameof(RequisicaoUsuario.Email)} é obrigatório.");
        }
    }
}
