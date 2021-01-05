using Livraria.Domain.Pessoas;

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Livraria.Domain.Usuarios
{
    public class Usuario
    {
        private string _senha;

        public int Id { get; set; }
        public int IdPessoa { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get => _senha; set => _senha = EncriptarSenha(value); }
        public Permissao Permissao { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public string EncriptarSenha(string value)
        {
            byte[] salt = Encoding.UTF8.GetBytes(Login);
            byte[] senhaByte = Encoding.UTF8.GetBytes(value);
            byte[] sha256 = new SHA256Managed().ComputeHash(senhaByte.Concat(salt).ToArray());
            return Convert.ToBase64String(sha256);
        }
    }
}
