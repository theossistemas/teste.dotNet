namespace Livraria.Exceptions
{
    public class LivroJaCadastradoExcepton : Exception
    {
        public LivroJaCadastradoExcepton(string nome)
            : base($"O livro '{nome}' já está cadastrado.") { }
    }
}
