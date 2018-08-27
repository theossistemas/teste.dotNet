namespace Livraria.Command
{
    public abstract class AutorCommand : Command
    {
        public string Id { get; protected set; }
        public string Nome { get; protected set; }
    }
}
