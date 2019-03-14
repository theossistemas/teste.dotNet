using livraria.Domain.entities.Specifications.UsuarioSpecification;
using livraria.Domain.entities.Validation.common;

namespace livraria.Domain.entities.Validation
{
    public class UsuarioLoginValidation : Validation<Usuario>
    {
        public UsuarioLoginValidation()
        {
            base.AddRule(new ValidationRule<Usuario>(new ValidaLogin(), "erro ao efetuar o login"));
        }
    }
}
