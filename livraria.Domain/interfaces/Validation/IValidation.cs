using livraria.Domain.entities.Validation.common;

namespace livraria.Domain.interfaces.Validation
{
    public interface IValidation<in TEntity>
    {
        ValidationResult Valid(TEntity entity);
    }
}