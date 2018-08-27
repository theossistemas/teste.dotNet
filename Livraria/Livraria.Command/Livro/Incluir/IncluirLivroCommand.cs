using Livraria.Domain.Entity;

namespace Livraria.Command
{
    public class IncluirLivroCommand : LivroCommand
    {
        public IncluirLivroCommand(string nome, string descricao, Autor autor, Editora editora, int? edicao)
        {
            Nome = nome;
            Descricao = descricao;
            Autor = autor;
            Editora = editora;
            Edicao = edicao;
        }

        public override bool IsValid()
        {
            ValidationResult = new IncluirLivroValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
