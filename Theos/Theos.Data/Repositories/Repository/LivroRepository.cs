using System;
using System.Collections.Generic;
using System.Text;
using Theos.Data.Contexto;
using Theos.Data.Repositories.Interface;
using Theos.Model.Model;

namespace Theos.Data.Repositories.Repository
{
    public class LivroRepository : ILivroRepository
    {
        private readonly AppDbContext _contexto;
        public LivroRepository(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<Livro> Livro => _contexto.Livro;

        public void NovoLivro(Livro livro)
        {
            _contexto.Add(livro);
            _contexto.SaveChanges();
        }

        public void AtualizaLivro(Livro livro)
        {
            _contexto.Update(livro);
            _contexto.SaveChanges();
        }

        public void DeletarLivro(Livro livro)
        {
                _contexto.Remove(livro);
                _contexto.SaveChanges();
        }
        public Livro BuscarLivroPorId(int id)
        {
            Livro livro = _contexto.Livro.Find(id);
            return livro;
        }

        
    }
}
