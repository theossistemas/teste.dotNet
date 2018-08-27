namespace Livraria.Command
{
    public class IncluirAutorCommand : AutorCommand
    {
        public IncluirAutorCommand(string nome)
        {
            Nome = nome;
        }

        public override bool IsValid()
        {
            ValidationResult = new IncluirAutorValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
