using Entities;
using Enumerators;
using Models.DTO;
using Repositories.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository repository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            this.repository = usuarioRepository;
        }

        public void Delete(Int64? id)
        {
            this.repository.Delete(id);
        }

        public UsuarioDTO Find(Int64? id)
        {
            Usuario usuario = this.repository.Find(id);

            if (usuario == null) return null;

            return new UsuarioDTO(usuario);
        }

        public IList<UsuarioDTO> FindAll()
        {
            IList<Usuario> usuarios = this.repository.FindAll();

            IList<UsuarioDTO> retorno = new List<UsuarioDTO>();

            usuarios.ToList().ForEach(x => retorno.Add(new UsuarioDTO(x)));

            return retorno;
        }

        public UsuarioDTO Save(UsuarioDTO dto)
        {
            Usuario usuario = new Usuario();

            usuario.Id = dto.Id;
            usuario.Login = dto.Login;
            usuario.Senha = dto.Senha;
            usuario.Permissao = dto.Permissao;

            return new UsuarioDTO(this.repository.Save(usuario));
        }

        public Boolean ValidarLogin(String login, String senha)
        {
            return this.repository.ValidarLogin(login, senha);
        }

        public void VerificarSeUsuarioJaCadastrado(String login)
        {
            this.repository.VerificarSeUsuarioJaCadastrado(login);
        }

        public Permissao? RetornarPermissaoDoUsuario(String login)
        {
            return this.repository.RetornarPermissaoDoUsuario(login);
        }
    }
}
