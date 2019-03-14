using livraria.Application.common;
using livraria.Application.interfaces;
using livraria.Domain.entities;
using livraria.Service.interfaces;

namespace livraria.Application
{
    public class UsuarioApplication : Application<Usuario>, IUsuarioApplication
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioApplication(IUsuarioService usuarioService) : base(usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public Usuario Login(Usuario usuario)
        {
            return _usuarioService.Login(usuario);
        }
    }
}
