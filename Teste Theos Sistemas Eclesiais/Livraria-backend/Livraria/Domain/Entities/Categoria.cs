namespace Livraria.Domain.Entities
{
    public class Categoria
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }

        // Construtor vazio para ORM/Serialização
        public Categoria() { }

        // Construtor de domínio
        public Categoria(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
        }

        public void AtualizarNome(string novoNome)
        {
            if (string.IsNullOrWhiteSpace(novoNome))
                throw new ArgumentException("Nome da categoria não pode ser vazio.");

            Nome = novoNome;
        }
    }
}
