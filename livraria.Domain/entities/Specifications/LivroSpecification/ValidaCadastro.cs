using Ilivraria.Domain.interfaces.Specification;

namespace livraria.Domain.entities.Specifications.LivroSpecification
{
    class ValidaCadastro : ISpecification<Livro>
    {
        public bool IsSatisfiedBy(Livro entity)
        {
            return !string.IsNullOrWhiteSpace(entity.Titulo)
               && entity.Valor > 0
               && entity.AutorId > 0
               && entity.CategoriaId > 0;

        }
    }
}
