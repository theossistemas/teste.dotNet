using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheosLivraria.Core.Queries.Ususarios
{
    public class UsuarioDTO(Guid id, string nome, string documento, string email, string telefone, DateTime dataNascimento, int perfil)
    {
        public Guid Id { get; set; } = id;
        public string Nome { get; set; } = nome;
        public string Documento { get; set; } = documento;
        public string Email { get; set; } = email;
        public string Telefone { get; set; } = telefone;
        public DateTime DataNascimento { get; set; } = dataNascimento;
        public int Perfil { get; set; } = perfil;

    }
}
