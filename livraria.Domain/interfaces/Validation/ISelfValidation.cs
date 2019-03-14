using livraria.Domain.entities.Validation.common;

namespace livraria.Domain.interfaces.Validation
{
    public interface ISelfValidation
    {
        ValidationResult ValidationResult { get; }
        bool IsValid { get; }
    }
}