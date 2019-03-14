using livraria.Domain.entities.Validation;
using livraria.Domain.entities.Validation.common;
using System.ComponentModel.DataAnnotations.Schema;

namespace livraria.Domain.entities
{
    public class Usuario : Validation<Usuario>
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Perfil Perfil { get; set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; set; } = new ValidationResult();

        [NotMapped]
        public bool IsValidLogin
        {
            get
            {
                var fiscal = new UsuarioLoginValidation();
                ValidationResult = fiscal.Valid(this);

                return ValidationResult.IsValid;
            }
        }
    }
}
