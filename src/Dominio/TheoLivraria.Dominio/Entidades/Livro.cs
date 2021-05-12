
namespace TheoLivraria.Dominio.Entidades
{
    public class Livro
    {
        public Livro(int id, string nome, string editora)
        {
            Id = id;
            Nome = nome;
            Editora = editora;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Editora { get; private set; }

        public void AtualizarDadosDoLivro(string nome, string editora)
        {
            this.Nome = nome;
            this.Editora = editora;
        }
    }
}
