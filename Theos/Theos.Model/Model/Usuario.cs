using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Theos.Model.Model
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        [StringLength(250)]
        public string Nome { get; set; }
        [StringLength(250)]
        public string Senha { get; set; }
    }
}
