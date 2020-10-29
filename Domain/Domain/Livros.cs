using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Livros
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Nome do Livro")]
        [DisplayFormat(NullDisplayText = "-")]
        public string NomeLivro { get; set; }

        [Required]
        [DisplayName("Data aquisição")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime DataAquisicao { get; set; }

        [Required]
        [DisplayName("Quantidade exemplares")]
        [DisplayFormat(NullDisplayText = "-")]
        public int QuantidadeExemplares { get; set; }
        #endregion
    }
}
