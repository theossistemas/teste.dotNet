using Models.DTO;

namespace Web.Models
{
    public class Sessao : ISessao
    {
        public UsuarioDTO usuario { get; set; }
    }
}
