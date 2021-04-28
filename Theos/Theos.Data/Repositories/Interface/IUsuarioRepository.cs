using System;
using System.Collections.Generic;
using System.Text;
using Theos.Model.Model;

namespace Theos.Data.Repositories.Interface
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> Usuario { get; }
        void SalvarNovoUsuario(Usuario usuario);
        void ApagarUsuario(int id);
       
    }
}
