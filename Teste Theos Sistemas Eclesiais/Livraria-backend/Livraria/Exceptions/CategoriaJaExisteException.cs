namespace Livraria.Exceptions
{
    public class CategoriaJaExisteException : Exception
    {
        public CategoriaJaExisteException(string nome)
            : base($"A categoria '{nome}' já está cadastrada.") { }
    }

    public class CategoriaNaoEncontradaException : Exception
    {
        public CategoriaNaoEncontradaException(Guid id)
            : base($"A categoria com Id '{id}' não foi encontrada.") { }
    }
}
