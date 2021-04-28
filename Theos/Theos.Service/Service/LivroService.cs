using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Theos.Data.Repositories.Interface;
using Theos.Model.Model;
using Theos.Service.Interface;

namespace Theos.Service.Service
{
    public class LivroService : ILivroService
    {
        private ILivroRepository _livroRepository;
        private ILogErroService _logErroService;

        public LivroService(ILivroRepository livroRepository, ILogErroService logErroService)
        {
            _livroRepository = livroRepository;
            _logErroService = logErroService;
        }
        public string ApagarLivro(int id)
        {
            try
            {
                Livro livro = _livroRepository.BuscarLivroPorId(id);
                _livroRepository.DeletarLivro(livro);
                return "Livro apagado com sucesso.";
            }
            catch (Exception e)
            {
                _logErroService.GravarErro(e.Message, DateTime.Now);
                return "Erro ao apagar o livro.";
            }
            
        }

        public IEnumerable<Livro> BuscarLivros()
        {
          
                IEnumerable<Livro> livros = _livroRepository.Livro;
                return livros;
          
        }

        public string NovoLivro(string nome)
        {
            try
            {
                Livro livros = new Livro();
                livros.NomeLivro = nome;
                _livroRepository.NovoLivro(livros);
                return "Livro cadastrado com sucesso.";
            }
            catch (Exception e)
            {
                _logErroService.GravarErro(e.Message, DateTime.Now);
                return "Não foi possível inserir um novo livro.";
            }
          
        }

        public string AtualizarLivro(int id, string nomeLivro)
        {
            try
            {
                Livro livro = _livroRepository.BuscarLivroPorId(id);
                livro.NomeLivro = nomeLivro;
                _livroRepository.AtualizaLivro(livro);
                return "Livro atualizado com sucesso";
            }
            catch (Exception e)
            {
                _logErroService.GravarErro(e.Message, DateTime.Now);
                return "Não foi possível atualiza o livro.";
            }
           
        }

        public bool LivroJaExiste(string nomeLivro)
        {
            var livros = _livroRepository.Livro;
            Livro livro = null;
            if (livros != null)
            {
                livro = livros.FirstOrDefault(x => x.NomeLivro.Trim().ToUpper() == nomeLivro.Trim().ToUpper());
                if (livro != null)
                {
                    return true;
                }
            }

            return false;
        }


    }
}
