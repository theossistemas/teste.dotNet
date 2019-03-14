using Ilivraria.Domain.interfaces.Specification;

namespace livraria.Domain.entities.Specifications.UsuarioSpecification
{
    public class ValidaLogin : ISpecification<Usuario>
    {
        public bool IsSatisfiedBy(Usuario entity)
        {
            return !string.IsNullOrWhiteSpace(entity.Email) || string.IsNullOrWhiteSpace(entity.Senha);
        }
    }
}
