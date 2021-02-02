using System;

namespace Theos.Livraria.Domain.Model.Usuario
{
    public class ResponseLoginUsuario
    { 
        public long  IdUsuario { get; set; }
        public string Token { get; set; }     
    }
}
