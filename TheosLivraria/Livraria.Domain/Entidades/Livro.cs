using Livraria.Common.Utils;
using FluentValidation;

namespace Livraria.Domain.Entidades
{
    public class Livro : Entity<Livro>
    {
        public string Titulo { get; private set; }
        public int AnoDePublicacao { get; private set; }
        public int Edicao { get; private set; }
        public int AutorId { get; private set; }
        public virtual Autor Autor { get; private set; }

        protected Livro() {}

        public Livro(string titulo, int anopublicacao, int edicao, Autor autor)
        {
            Titulo = titulo;
            AnoDePublicacao = anopublicacao;
            Edicao = edicao;
            Autor = autor;
        }

        public override bool Validar()
        {
            RuleFor(x => x.Titulo)
             .NotEmpty()
             .NotNull()
             .MaximumLength(Constantes.QuantidadeDeCaracteres200);

            RuleFor(x => x.AnoDePublicacao)
                .GreaterThan(Constantes.Zero);

            RuleFor(x => x.Edicao)
                .GreaterThan(Constantes.Zero);

            RuleFor(x => x.Autor)
                .NotNull()
                .When(a => a.AutorId == Constantes.Zero);

            RuleFor(x => x.AutorId)
                .GreaterThan(Constantes.Zero)
                .When(a => a.Autor == null);


            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public void AlterarTitulo(string titulo)
        {
            Titulo = titulo;
        }

        public void AlterarAnoDePublicao(int anoPublicacao)
        {
            AnoDePublicacao = anoPublicacao;
        }

        public void AlterarEdicao(int edicao)
        {
            Edicao = edicao;
        }

        public void AlterarAutorId(int id)
        {
            AutorId = id;
        }

        public void AlterarAutor(Autor autor)
        {
            if (autor == null) return;
            Autor = autor;
        }
    }
}
