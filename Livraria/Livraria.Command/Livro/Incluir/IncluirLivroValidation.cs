namespace Livraria.Command
{
    public class IncluirLivroValidation : LivroValidation<IncluirLivroCommand>
    {
        public IncluirLivroValidation()
        {
            ValidateNome();
            ValidateDescricao();
            ValidateAutor();
            ValidateEditora();
            ValidateEdicao();
        }
    }
}
