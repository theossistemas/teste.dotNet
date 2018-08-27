namespace Livraria.Command
{
    public class ExcluirLivroCommand : LivroCommand
    {
        public ExcluirLivroCommand(string id)
        {
            Id = id;
        }
        public override bool IsValid()
        {
            ValidationResult = new ExcluirLivroValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
