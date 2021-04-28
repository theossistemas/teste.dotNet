using System.Collections.Generic;
using Theos.Model.Model;

namespace Theos.Service.Interface
{
    public interface IUsuarioService
    {
        IEnumerable<Usuario> BuscarUsuario();
        string NovoUsuario(string nome, string senha);
        bool UsuarioAutenticado(string nome, string senha);
    }
}
