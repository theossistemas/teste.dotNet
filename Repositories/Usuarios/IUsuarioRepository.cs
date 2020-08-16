using Entities;
using Enumerators;
using Repositories.Base;
using System;

namespace Repositories.Usuarios
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Boolean ValidarLogin(String login, String senha);

        Permissao? RetornarPermissaoDoUsuario(String login);

        Usuario FindUserByLogin(String login);

        void VerificarSeUsuarioJaCadastrado(String login);
    }
}