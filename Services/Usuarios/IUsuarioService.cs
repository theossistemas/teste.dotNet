using Enumerators;
using Models.DTO;
using Services.Base;
using System;

namespace Services.Usuarios
{
    public interface IUsuarioService : IService<UsuarioDTO>
    {
        Boolean ValidarLogin(String login, String senha);

        Permissao? RetornarPermissaoDoUsuario(String login);

        UsuarioDTO FindUserByLogin(String login);

        void VerificarSeUsuarioJaCadastrado(String login);
    }
}
