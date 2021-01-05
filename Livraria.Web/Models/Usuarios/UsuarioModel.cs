using Livraria.Domain.Usuarios;
using Livraria.Web.Models.Pessoas;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Web.Models.Usuarios
{
    public class UsuarioModel
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public Permissao Permissao { get; set; }

        public virtual PessoaModel Pessoa { get; set; }
    }
}
