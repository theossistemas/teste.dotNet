using Livraria.Domain;

namespace Livraria.DTOs
{
    public class LivroDTO
    {
        #region[Propriedades]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }
        public int AnoPublicacao { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public DateTime? CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public DateTime? ExcluidoEm { get; set; }
        public bool Ativo { get; set; }
        #endregion

        #region[Construtor]
        // construtor que recebe a entidade Livro (domain)
        public LivroDTO(Livro livro)
        {
            Id = livro.Id;
            Titulo = livro.Titulo;
            Autor = livro.Autor;
            ISBN = livro.ISBN;
            AnoPublicacao = livro.AnoPublicacao;
            Quantidade = livro.Quantidade;
            Preco = livro.Preco;
            CriadoEm = livro.CriadoEm;
            AtualizadoEm = livro.AtualizadoEm;
            ExcluidoEm = livro.ExcluidoEm;
            Ativo = livro.Ativo;
        }
        public LivroDTO() {}

        #endregion
    }
}
