using Dapper.Contrib.Extensions;

namespace biblioteca.Services
{
    [Table("dbo.Livros")]
    public class Book
    {
        [ExplicitKey]
        public string id { get; set; }
        public string nome { get; set; }
        public string descricao{ get; set; }
       
    }
}


