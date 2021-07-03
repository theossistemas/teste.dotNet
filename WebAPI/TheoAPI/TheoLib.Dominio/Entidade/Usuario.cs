using Dapper.Contrib.Extensions;

namespace TheoLib.Dominio.Entidade
{
    [Table("Usuario")]
    public class Usuario
    {
        [ExplicitKey]
        [Computed]
        public long Id { get; set; } 
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
