using livraria.Domain.entities.Specifications.LivroSpecification;
using livraria.Domain.entities.Validation.common;

namespace livraria.Domain.entities.Validation
{
    class LivroAlteracaoValidation : Validation<Livro>
    {
        public LivroAlteracaoValidation()
        {
            base.AddRule(new ValidationRule<Livro>(new ValidaAlteracao(), "erro ao alterar o registro"));
        }
    }
}
