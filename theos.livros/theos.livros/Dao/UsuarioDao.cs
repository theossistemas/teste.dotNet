using theos.livros.Entitys;
using theos.livros.Models;

namespace theos.livros.Dao
{
    public class UsuarioDao
    {
        public Usuario Consultar(int id)
        {
            using (var _context = new UsuarioContext())
            {
                return _context.Usuarios.Find(id);
            };
        }
    }
}
