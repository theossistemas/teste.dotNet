using Newtonsoft.Json;
using System;

namespace Theos.Livraria.Domain.Model.Usuario
{
    public class ResponseUsuario
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; } 
        public bool PrimeiroAcesso { get; set; }
        public bool Ativo { get; set; } 
    }
}
