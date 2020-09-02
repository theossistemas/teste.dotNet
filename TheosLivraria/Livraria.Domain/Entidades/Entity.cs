using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livraria.Domain.Entidades
{
    public abstract class Entity<TEntity> : AbstractValidator<TEntity>
         where TEntity : Entity<TEntity>
    {
        public int Id { get; protected set; }

        public void DefinirId(int id)
        {
            Id = id;
        }

        [NotMapped]
        public ValidationResult ValidationResult { get; protected set; }

        public abstract bool Validar();
    }
}
