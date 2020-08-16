using Microsoft.AspNetCore.Authorization;
using Repositories.Usuarios;
using System;
using System.Threading.Tasks;

namespace Services.Acesso
{
    public class PermissaoAcessoHandler : AuthorizationHandler<PermissaoAcessoRequirement>
    {
        private IUsuarioRepository repository;

        public PermissaoAcessoHandler(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissaoAcessoRequirement requirement)
        {
            if (context.User == null) return null;

            Boolean autorizado = requirement.Permissao.Equals(this.repository.RetornarPermissaoDoUsuario(context.User.Identity.Name));

            if (autorizado)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
