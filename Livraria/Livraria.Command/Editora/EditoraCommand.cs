namespace Livraria.Command
{
    public abstract class EditoraCommand : Command
    {
        public string Id { get; protected set; }
        public string Nome { get; protected set; }
    }
}
