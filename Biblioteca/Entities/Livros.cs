namespace WebApi.Entities
{
    public class Livro     
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public int Ano { get; set; }
        public string Autor { get; set; }
        public string DataFabricacao { get; set; }
    }
}