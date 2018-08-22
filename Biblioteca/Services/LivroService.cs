using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Repository;

namespace WebApi.Services
{
    public interface ILivroService
    {
        IEnumerable<Livro> GetAll();
        Livro GetById(long id);
        Livro Create(Livro livro);
        void Update(Livro livro);
        void Delete(long id);
    }

    public class LivroService : ILivroService
    {
        private DataContext _context;
        private LivroRepository Repository { get; set; } = new LivroRepository();

        public LivroService(DataContext context)
        {
            _context = context;
        }

        public Livro GetLivroNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                return null;
            var livro = this.Repository.GetLivroNome(nome);
            // check if username exists
            if (livro == null)
                return null;
            // authentication successful
            return livro;
        }

        public IEnumerable<Livro> GetAll()
        {
            return this.Repository.GetLivros();
        }

        public Livro GetById(long id)
        {
            return this.Repository.GetLivroById(id);
        }

        public Livro Create(Livro livro)
        {
            // validation
            if (string.IsNullOrWhiteSpace(livro.Nome))
                throw new AppException("Password is required");

            if (this.Repository.GetLivros().Any(x => x.Nome == livro.Nome))
                throw new AppException("Livro \"" + livro.Nome + "\" is already taken");

            this.Repository.Save(livro);

            return livro;
        }

        public void Update(Livro livroParam)
        {
            var livro = this.Repository.GetLivroById(livroParam.Id);

            if (livro == null)
                throw new AppException("User not found");

            if (livroParam.Nome != livro.Nome)
            {
                // username has changed so check if the new username is already taken
                if (this.Repository.GetLivros().Any(x => x.Nome == livroParam.Nome))
                    throw new AppException("Livro " + livroParam.Nome + " is already taken");
            }

            // update user properties
            livro.Nome = livroParam.Nome;
            livro.Ano = livroParam.Ano;
            livro.Autor = livroParam.Autor;
            livro.DataFabricacao = livroParam.DataFabricacao;

            this.Repository.Update(livro);
        }

        public void Delete(long id)
        {
            this.Repository.Delete(id);
        }
    }
}

