using livraria.Domain.entities;
using livraria.Domain.interfaces;
using livraria.Service.common;
using livraria.Service.interfaces;

namespace livraria.Service
{
    public class UsuarioService : Service<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
            : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuario Login(Usuario usuario)
        {
            return _usuarioRepository.Login(usuario);
        }
    }
}
