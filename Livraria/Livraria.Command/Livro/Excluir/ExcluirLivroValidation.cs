namespace Livraria.Command
{
    public class ExcluirLivroValidation : LivroValidation<ExcluirLivroCommand>
    {
        public ExcluirLivroValidation()
        {
            ValidateId();
        }
    }
}
