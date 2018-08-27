using Livraria.Domain.Entity;

namespace Livraria.Command
{
    public abstract class LivroCommand : Command
    {
        public string Id { get; protected set; }
        public string Nome { get; protected set; }
        public string Descricao { get; protected set; }
        public Autor Autor { get;  set; }
        public Editora Editora { get;  set; }
        public int? Edicao { get; protected set; }
    }
}
