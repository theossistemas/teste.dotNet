namespace Livraria.Command
{
    public class IncluirAutorValidation : AutorValidation<IncluirAutorCommand>
    {
        public IncluirAutorValidation()
        {
            ValidateNome();
        }
    }
}
