using Livraria.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Livraria.Domain
{
    public class Livro : IEntityBase, ILivro
    {
        #region[Propriedades]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }
        public int AnoPublicacao { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public override DateTime CriadoEm { get; set; }
        public override DateTime? AtualizadoEm { get; set; }
        public override DateTime? ExcluidoEm { get; set; }
        public override bool Ativo { get; set; }
        #endregion

        #region[Construtores]
        public Livro()
        {
            
        }

        public Livro(int id, string titulo, string autor, string iSBN, int anoPublicacao, int quantidade, decimal preco)
        {
            Id = id;
            Titulo = titulo;
            Autor = autor;
            ISBN = iSBN;
            AnoPublicacao = anoPublicacao;
            Quantidade = quantidade;
            Preco = preco;
        }
        #endregion
    }
}
