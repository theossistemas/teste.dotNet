using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheosLivraria.Core.Queries.Ususarios
{
    public class UsuarioGridDTO(Guid id, string nome, string email, string documento, string telefone)
    {
        public Guid Id { get; set; } = id;
        public string Nome { get; set; } = nome;
        public string Email { get; set; } = email;
        public string Documento { get; set; } = documento;
        public string Telefone { get; set; } = telefone;
    }
}
