using LivrariaTheos.Core.DomainObjects;
using LivrariaTheos.Estoque.Domain.Livros;
using System.Collections.Generic;

namespace LivrariaTheos.Estoque.Domain.Generos
{
    public class Genero : Entity<int, Genero>
    {
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }

        private readonly List<Livro> _livros;
        public IReadOnlyCollection<Livro> Livros => _livros;

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public void AlterarNome(string nome)
        {            
            Nome = nome;
            Validar();
        }

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome do genero não pode estar vazio");
            Validacoes.ValidarSeMaiorQue(Nome.Length, 50, "O campo Nome do genero não pode conter mais do que 50 caracteres");
        }
    }
}
