using LivrariaTheos.Core;
using LivrariaTheos.Core.DomainObjects;
using LivrariaTheos.Estoque.Domain.Autores;
using LivrariaTheos.Estoque.Domain.Generos;

namespace LivrariaTheos.Estoque.Domain.Livros
{
    public class Livro : Entity<int, Livro>, IAggregateRoot
    {
        public int AutorId { get; set; }
        public int GeneroId { get; set; }
        public string Nome { get; private set; }
        public string Sinopse { get; private set; }       
        public int QuantidadePaginas { get; private set; }        
        public string CaminhoCapa { get; private set; }
        public string NomeCapa { get; private set; }
        public bool Ativo { get; private set; }

        public virtual Autor Autor { get; set; }
        public virtual Genero Genero { get; private set; }

        protected Livro() { }       

        public Livro(int autorId, int generoId, string nome, string sinopse, int quantidadePaginas, bool ativo)
        {
            AutorId = autorId;
            GeneroId = generoId;
            Nome = nome;
            Sinopse = sinopse;
            QuantidadePaginas = quantidadePaginas;
            CaminhoCapa = Resources.CaminhoCapas;            
            Ativo = ativo;       
            
            Validar();
        }

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public void AlterarNome(string nome)
        {
            Validacoes.ValidarSeVazio(nome, "O campo Nome do livro não pode estar vazio");
            Nome = nome;
        }

        public void AlterarSinopse(string sinopse)
        {
            Validacoes.ValidarSeVazio(sinopse, "O campo Sinopse do livro não pode estar vazio");
            Sinopse = sinopse;
        }

        public void AlterarQuantidadePaginas(int quantidadePaginas)
        {
            Validacoes.ValidarSeIgual(QuantidadePaginas, 0, "O campo QuantidadePaginas do livro não pode ser 0");
            QuantidadePaginas = quantidadePaginas;
        }

        public void AlterarCaminhoCapa(string caminhoCapa)
        {           
            CaminhoCapa = caminhoCapa;
        }

        public void VincularGenero(Genero genero)
        {
            Genero = genero;
            GeneroId = genero.Id;

            Validar();
        }

        public void AlterarGenero(Genero genero)
        {
            Genero = genero;
            GeneroId = genero.Id;
        }

        public void VincularAutor(Autor autor)
        {
            Autor = autor;
            AutorId = autor.Id;

            Validar();
        }

        public void AlterarAutor(Autor autor)
        {
            Autor = autor;
            AutorId = autor.Id;
        }

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome do livro não pode estar vazio");
            Validacoes.ValidarSeMaiorQue(Nome.Length, 150,"O campo Nome do livro não pode conter mais do que 150 caracteres");
            Validacoes.ValidarSeVazio(Sinopse, "O campo Sinopse não pode estar vazio");
            Validacoes.ValidarSeMaiorQue(Sinopse.Length, 2000, "O campo Nome do não pode conter mais do que 2000 caracteres");
            Validacoes.ValidarSeIgual(QuantidadePaginas, 0, "O campo QuantidadePaginas do livro não pode ser 0");
            Validacoes.ValidarSeIgual(AutorId, 0, "O campo AutoId do livro não pode ser 0");
        }
    }
}