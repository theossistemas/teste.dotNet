namespace Livraria.Command
{
    public class IncluirEditoraCommand : EditoraCommand
    {
        public IncluirEditoraCommand(string nome)
        {
            Nome = nome;
        }

        public override bool IsValid()
        {
            ValidationResult = new IncluirEditoraValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
