using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class Sessao
    {
        private Sessao() { }

        public static UsuarioDTO Usuario { get; set; }
    }
}
