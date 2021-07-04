using System;
using System.ComponentModel.DataAnnotations;

namespace Livraria.Domain.Entities.Administracao
{
    public class Usuario : BaseEntity
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public TipoUsuario Role { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Equals(obj as Usuario);
        }

        public bool Equals(Usuario obj)
        {
            if (obj == null)
                return false;

            return Equals(obj.Id, Id)
                && Equals(obj.Email, Email)
                && Equals(obj.Nome, Nome)
                && Equals(obj.Role, Role);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Tuple.Create(Id, Email, Nome, Role).GetHashCode();
            }
        }
    }
}
