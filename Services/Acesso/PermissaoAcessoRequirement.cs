using Enumerators;
using Microsoft.AspNetCore.Authorization;

namespace Services.Acesso
{
    public class PermissaoAcessoRequirement : IAuthorizationRequirement
    {
        public PermissaoAcessoRequirement(Permissao permissao)
        {
            this.Permissao = permissao;
        }

        public Permissao Permissao { get; }
    }
}
