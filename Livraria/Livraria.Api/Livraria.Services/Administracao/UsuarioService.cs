using Livraria.Domain.Entities.Administracao;
using Livraria.Infra.Data.Interfaces.Repositories.Administracao;
using Livraria.Services.Interfaces.Administracao;
using System.Threading.Tasks;

namespace Livraria.Services.Administracao
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository _usuarioRepository)
        {
            this._usuarioRepository = _usuarioRepository;
        }

        public async Task<Usuario> Autenticar(string email, string senha)
        {
            return await _usuarioRepository.Autenticar(email, senha);
        }

        public async Task Registrar(Usuario usuario)
        {
            await _usuarioRepository.Create(usuario);
        }
    }
}
