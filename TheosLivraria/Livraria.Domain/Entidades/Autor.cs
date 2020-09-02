using FluentValidation;
using Livraria.Common.Utils;
using System.Collections.Generic;

namespace Livraria.Domain.Entidades
{
    public class Autor : Entity<Autor>
    {
        public string Nome { get; private set; }
        public virtual IList<Livro> Livros { get; private set; }

        protected Autor() {}

        public Autor(string nome)
        {
            Nome = nome;
        }

        public override bool Validar()
        {
            RuleFor(x => x.Nome)
            .NotEmpty()
            .NotNull()
            .MaximumLength(Constantes.QuantidadeDeCaracteres100);

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public void AlterarNome(string nome)
        {
            Nome = nome;
        }

        public void AlterarLivros(List<Livro> livros)
        {
            if (livros == null) return;
            Livros = livros;
        }
    }
}
