using Business.Services.Interface;
using Persistence.Entity;
using Persistence.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Services
{
    public class LivrosServices : ILivrosServices
    {
        public readonly ILivroRepository _livroRepository;

        public LivrosServices(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public void AddLivro(Livro livro)
        {
            if (livro.Id != 0 && (GetById(livro.Id) != null))
                throw new ArgumentException("Já existe um livro com esse ID!");

            if (_livroRepository.GetByField(x => x.Descricao.Equals(livro.Descricao)) != null)
                throw new ArgumentException("Já existe um livro com essa descrição!");

            _livroRepository.Add(livro);
        }

        public Livro EditLivro(int id, Livro livro)
        {
            var livroDb = GetById(id);
           return _livroRepository.Edit(livroDb, livro);
        }

        public IEnumerable<Livro> GetAll()
        {
            return _livroRepository.Get().OrderBy(i => i.Descricao);
        }

        public Livro GetById(int id)
        {
            var livroDb = _livroRepository.GetById(id);
            if (livroDb == null)
                throw new ArgumentException("Livro não encontrado!");

            return livroDb;
        }

        public bool Save()
        {
            return _livroRepository.Save();
        }

        public void RemoveLivro(int id)
        {
            var livroDb = GetById(id);
            _livroRepository.Remove(livroDb);
        }
    }
}
