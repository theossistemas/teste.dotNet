using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Core.Sergurancao;
using TheosLivraria.Core.Util;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.Core.Command.Seguranca
{
    public class LoginCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
    {   public async Task<Retorno<string>> ExecuteAsync(LoginCommand command)
        {
            var usuario = await unitOfWork.UsuarioRepository.ObterPorEmail(command.Email);
            if (usuario == null)
                return Retorno<string>.Falha("Usuário ou senha inválidos");

            var senhaHash = Usuario.GerarHash(command.Senha);

            if (usuario.Senha != senhaHash)
                return Retorno<string>.Falha("Usuário ou senha inválidos", null, HttpStatusCode.Unauthorized);

            var token = tokenService.GerarToken(usuario);
            return Retorno<string>.Ok(token, "Login realizado com sucesso");
        }
    }
}
