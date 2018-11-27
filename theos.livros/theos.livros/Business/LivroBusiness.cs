using System.Collections.Generic;
using theos.livros.Business.Interfaces;
using theos.livros.Dao;
using theos.livros.Entitys;

namespace theos.livros.Business
{
    public class LivroBusiness : ILivroBusiness
    {
        private readonly LivroDao _livroDao;

        public LivroBusiness()
        {
            _livroDao = new LivroDao();
        }

        public List<Livro> ListarLivros()
        {
            return _livroDao.Listar();
        }

        public bool Inserir(Livro livro)
        {
            var _registro = _livroDao.Consultar(livro.IdLivro);

            if (_registro == null)
            {
                _livroDao.InserirLivro(livro);
            }

            return _registro == null;
        }

        public bool Atualizar(Livro livro)
        {
            var _registro = _livroDao.Consultar(livro.IdLivro);

            if (_registro != null)
            {
                _livroDao.AtualizarLivro(livro);
            }

            return _registro != null;
        }

        public bool Remover(int id)
        {
            var _registro = _livroDao.Consultar(id);

            if (_registro != null)
            {
                _livroDao.RemoverLivro(id);
            }

            return _registro != null;
        }
    }
}
