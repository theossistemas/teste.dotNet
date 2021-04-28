using System;
using System.Collections.Generic;
using System.Text;
using Theos.Data.Contexto;
using Theos.Data.Repositories.Interface;
using Theos.Model.Model;

namespace Theos.Data.Repositories.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _contexto;
        public UsuarioRepository(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<Usuario> Usuario => _contexto.Usuario;

        public void ApagarUsuario(Usuario usuario)
        {
            _contexto.Remove(usuario);
            _contexto.SaveChanges();
        }

        public void ApagarUsuario(int id)
        {
            throw new NotImplementedException();
        }

        public void SalvarNovoUsuario(Usuario usuario)
        {
            _contexto.Add(usuario);
            _contexto.SaveChanges();
        }

        //public Usuario GetUsuarioNomeSenha(string nome, string senha)
        //{
        //    var usuario = _contexto.Usuario;
        //    usuario.where
        //    return usuario;
        //}

    }

}
