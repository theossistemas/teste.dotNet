using LivrariaTheos.Core.DomainObjects;
using LivrariaTheos.Estoque.Domain.Livros;
using System.Collections.Generic;

namespace LivrariaTheos.Estoque.Domain.Autores
{
    public class Autor : Entity<int, Autor>, IAggregateRoot
    {
        public string Nome { get; private set; }
        public int Nacionalidade { get; private set; }
        public string InformacoesRelevantes { get; private set; }
        public bool Ativo { get; set; }

        private readonly List<Livro> _livros;

        protected Autor()
        {

        }

        public Autor(string nome, int nacionalidade, string informacoesRelevantes, bool ativo)
        {
            Nome = nome;
            Nacionalidade = nacionalidade;
            InformacoesRelevantes = informacoesRelevantes;
            Ativo = ativo;
        }

        public IReadOnlyCollection<Livro> Livros => _livros;

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public void AlterarNome(string nome)
        {
            //TODO: Validar o tamanho do campo
            Validacoes.ValidarSeVazio(nome, "O campo Nome do autor não pode estar vazio");
            Nome = nome;
        }

        public void AlterarNacionalidade(int nacionalidade)
        {          
            Validacoes.ValidarSeIgual(nacionalidade, 0, "O campo Nacionalidade do autor não pode ser igual a 0");
            Nacionalidade = nacionalidade;
        }

        public void AlterarInformacoesRelevantes(string informacoesRelevantes)
        {
            //TODO: Validar o tamanho do campo
            Validacoes.ValidarSeMaiorQue(informacoesRelevantes.Length, 2000, "O campo InformacoesRelevantes do autor não pode conter mais que 2000 caracteres");
            InformacoesRelevantes = informacoesRelevantes;
        }
      
        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome do autor não pode estar vazio");
            Validacoes.ValidarSeMaiorQue(Nome.Length, 150, "O campo Nome do autor não pode conter mais do que 150 caracteres"); 
            Validacoes.ValidarSeIgual(Nacionalidade, 0, "O campo Nacionalidade do autor não pode ser igual a 0");
            Validacoes.ValidarSeMaiorQue(InformacoesRelevantes.Length, 2000, "O campo InformacoesRelevantes do autor não pode conter mais que 2000 caracteres");
        }
    }
}
