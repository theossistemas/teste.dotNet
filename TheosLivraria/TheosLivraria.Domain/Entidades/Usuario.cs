using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheosLivraria.Domain.Entidades
{
    public class Usuario : EntidadeBase<int>
    {
        public string Nome { get; private set; }
        public string Documento { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public string Telefone { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public Perfil Perfil { get; private set; }

        public Usuario() { }

        public Usuario(string nome, string documento, string email, string senha, string telefone, DateTime dataNascimento, Perfil perfil)
        {
            Validar(nome, documento, email, senha, telefone, dataNascimento);

            Nome = nome;
            Documento = documento;
            Email = email;
            Senha = GerarHash(senha);
            Telefone = telefone;
            DataNascimento = dataNascimento;
            Perfil = perfil;
        }

        public void AlterarDados(string nome, string documento, string email, string telefone, DateTime dataNascimento)
        {
            Validar(nome, documento, email, Senha, telefone, dataNascimento);

            Nome = nome;
            Documento = documento;
            Email = email;
            Telefone = telefone;
            DataNascimento = dataNascimento;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void AlterarSenha(string novaSenha)
        {
            if (string.IsNullOrWhiteSpace(novaSenha) || novaSenha.Length < 6)
                throw new ArgumentException("Senha deve ter pelo menos 6 caracteres.");

            Senha = GerarHash(novaSenha);
            DataAtualizacao = DateTime.UtcNow;
        }

        public static string GerarHash(string senha)
        {
            senha = senha.Trim();
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(senha);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private static void Validar(string nome, string documento, string email, string senha, string telefone, DateTime dataNascimento)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório.");
            if (string.IsNullOrWhiteSpace(documento))
                throw new ArgumentException("Documento é obrigatório.");
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("E-mail inválido.");
            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6)
                throw new ArgumentException("Senha deve ter pelo menos 6 caracteres.");
            if (string.IsNullOrWhiteSpace(telefone))
                throw new ArgumentException("Telefone é obrigatório.");
            if (dataNascimento > DateTime.UtcNow)
                throw new ArgumentException("Data de nascimento inválida.");
        }

    }
}
