using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using theos.livros.Dao;
using theos.livros.Entitys;

namespace theos.livros.Business
{
    public class UsuarioBusiness
    {
        private readonly UsuarioDao _usuarioDao;

        public UsuarioBusiness()
        {
            _usuarioDao = new UsuarioDao();
        }

        public Usuario ConsultarUsuario(int id)
        {
            return _usuarioDao.Consultar(id);
        }
    }
}
