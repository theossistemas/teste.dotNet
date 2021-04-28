using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Theos.Data.Repositories.Interface;
using Theos.Model.Model;
using Theos.Service.Interface;

namespace Theos.Service.Service
{

    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository _usuarioRepository;
        private ILogErroService _logErroService;
        public UsuarioService(IUsuarioRepository usuarioRepository, ILogErroService logErroService)
        {
            _usuarioRepository = usuarioRepository;
            _logErroService = logErroService;

        }
        public void AtualizarUsuario(int id, string nome, string senha)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> BuscarUsuario()
        {
            throw new NotImplementedException();
        }

        public bool UsuarioAutenticado(string nome, string senha)
        {
            var usuarios = _usuarioRepository.Usuario;
            var senhaCodificada = CalculaHash(senha);
            Usuario usuario = null;
            if (usuarios != null)
            {
                usuario = usuarios.Where(x => x.Nome.Trim().ToUpper() == nome.Trim().ToUpper() && x.Senha == senhaCodificada.Trim()).FirstOrDefault();
                if (usuario != null)
                {
                    return true;
                }
            }

            return false;
        }

        public string NovoUsuario(string nome, string senha)
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario.Nome = nome;
                usuario.Senha = CalculaHash(senha);
                _usuarioRepository.SalvarNovoUsuario(usuario);
                return "Salvo com sucesso!";
            }
            catch (Exception e)
            {
                _logErroService.GravarErro(e.Message, DateTime.Now);
                return "Erro criar um novo usuário.";
            }
        }

        public static string CalculaHash(string Senha)
        {
            try
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(Senha);
                byte[] hash = md5.ComputeHash(inputBytes);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString(); 
            }
            catch (Exception)
            {
                return null; 
            }

        }
    }
}
