using Entities;
using Models.DTO;
using Repositories.Livros;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Livros
{
    public class LivroService : ILivroService
    {
        private ILivroRepository repository;

        public LivroService(ILivroRepository livroRepository)
        {
            this.repository = livroRepository;
        }

        public void Delete(Int64? id)
        {
            repository.Delete(id);
        }

        public LivroDTO Find(Int64? id)
        {
            Livro livro = repository.Find(id);

            if (livro == null) return null;

            return new LivroDTO(livro);
        }

        public IList<LivroDTO> FindAll()
        {
            IList<Livro> livros = repository.FindAll();

            IList<LivroDTO> retorno = new List<LivroDTO>();

            livros.ToList().ForEach(x => retorno.Add(new LivroDTO(x)));

            return retorno;
        }

        public LivroDTO Save(LivroDTO dto)
        {
            Livro livro = new Livro();

            livro.Id = dto.Id;
            livro.Titulo = dto.Titulo;
            livro.Descricao = dto.Descricao;

            return new LivroDTO(repository.Save(livro));
        }

        public void VerificarSeLivroNaoExiste(LivroDTO livro)
        {
            this.repository.VerificarSeLivroNaoExiste(livro);
        }
    }
}
