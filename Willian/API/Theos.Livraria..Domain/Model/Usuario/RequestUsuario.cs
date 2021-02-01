using System;

namespace Theos.Livraria.Domain.Model.Usuario
{
    public class RequestUsuario
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }    
        public bool Ativo { get; set; } 
    }
}
