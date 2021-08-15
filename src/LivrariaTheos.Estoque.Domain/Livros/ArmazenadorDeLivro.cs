using LivrariaTheos.Core.DomainObjects;
using LivrariaTheos.Estoque.Domain.Livros.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaTheos.Estoque.Domain.Livros
{
    public class ArmazenadorDeLivro
    {
        private readonly ILivroRepository _livroRepositorio;

        public ArmazenadorDeLivro(ILivroRepository livroRepositorio)           
        {
            _livroRepositorio = livroRepositorio;
           
        }

        public async Task<Livro> Armazenar(Livro livro)
        {         
            var livroExistente = await LivroJaExistente(livro);

            if (livroExistente!= null && livroExistente.Id != livro.Id)
            {
                throw new DomainException("Livro já cadastrado.");
            }            

            if (livro.Id == 0)
            {
                livro.Validar();

                _livroRepositorio.Adicionar(livro);
            }                
            else
            {
                livro = await AlterarLivro(livro);

                _livroRepositorio.Atualizar(livro);
            }

            await _livroRepositorio.UnitOfWork.Commit();

            return livro;
        }

        public async Task Excluir(int id)
        {
            var livro = await _livroRepositorio.ObterPorId(id);

            if (livro == null)
                throw new DomainException("Livro não encontrado.");

            _livroRepositorio.Excluir(livro);

            await _livroRepositorio.UnitOfWork.Commit();
        }

        private async Task<Livro> AlterarLivro(Livro livroAlterado)
        {
            var livro = await _livroRepositorio.ObterPorId(livroAlterado.Id);
            livro.AlterarNome(livroAlterado.Nome);
            livro.AlterarSinopse(livroAlterado.Sinopse);
            livro.AlterarQuantidadePaginas(livroAlterado.QuantidadePaginas);
            livro.AlterarCaminhoCapa(livroAlterado.CaminhoCapa);
            livro.AlterarDataAlteracao(DateTime.Now);
            livro.AlterarUsuarioAlteracao("Administrador");

            return livro;
        }

        private async Task<Livro> LivroJaExistente(Livro livro)
        {
            var livros = await _livroRepositorio.ObterPorNome(livro.Nome);

           return livros.Where(l => l.AutorId == livro.AutorId)
                .FirstOrDefault();
        }
    }
}
