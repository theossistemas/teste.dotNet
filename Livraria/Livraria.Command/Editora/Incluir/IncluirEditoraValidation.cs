namespace Livraria.Command
{
    internal class IncluirEditoraValidation : EditoraValidation<IncluirEditoraCommand>
    {
        public IncluirEditoraValidation()
        {
            ValidateNome();
        }
    }
}