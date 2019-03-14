using livraria.Domain.entities.Specifications.LivroSpecification;
using livraria.Domain.entities.Validation.common;

namespace livraria.Domain.entities.Validation
{
    class LivroCadastroValidation : Validation<Livro>
    {
        public LivroCadastroValidation()
        {
            base.AddRule(new ValidationRule<Livro>(new ValidaCadastro(), "erro ao efetuar o cadastro"));
        }
    }
}
