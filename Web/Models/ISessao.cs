using Models.DTO;

namespace Web.Models
{
    public interface ISessao
    {
        UsuarioDTO usuario { get; set; }
    }
}