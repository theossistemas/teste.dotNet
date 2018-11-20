using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Models.Entidades
{

    [Table("Livro")]
    public class Livro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LivroId { get; set; }

        [Display(Name ="Título do Livro")]
        public string LivroNome { get; set; }

        [Display(Name = "Autor")]
        public string LivroAutor { get; set; }

        [DisplayFormat( DataFormatString = "{0:MM/dd/yyyy}")]        
        [DataType(DataType.Date)]
        [Display(Name = "Data de Publicação")]
        public DateTime LivroDataPublicacao { get; set; }



    }
}
