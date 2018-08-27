using Livraria.Domain.Entity;

namespace Livraria.Command
{
    public class AlterarLivroCommand : LivroCommand
    {
        public AlterarLivroCommand(string id, string nome, string descricao, Autor autor, Editora editora, int? edicao)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Autor = autor;
            Editora = editora;
            Edicao = edicao;
        }

        public override bool IsValid()
        {
            ValidationResult = new AlterarLivroValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
