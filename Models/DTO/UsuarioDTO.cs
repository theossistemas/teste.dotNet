using Entities;

namespace Models.DTO
{
    public class UsuarioDTO : Usuario
    {
        public UsuarioDTO() { }

        public UsuarioDTO(Usuario usuario)
        {
            this.Id = usuario.Id;
            this.Login = usuario.Login;
            this.Senha = usuario.Senha;
            this.Permissao = usuario.Permissao;
        }
    }
}
