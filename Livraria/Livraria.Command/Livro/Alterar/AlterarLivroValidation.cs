namespace Livraria.Command
{
    public class AlterarLivroValidation : LivroValidation<AlterarLivroCommand>
    {
        public AlterarLivroValidation()
        {
            ValidateId();
            ValidateNome();
            ValidateDescricao();
            ValidateAutor();
            ValidateEditora();
            ValidateEdicao();
        }
    }
}
