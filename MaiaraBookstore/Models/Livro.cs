using System;
using System.ComponentModel.DataAnnotations;

namespace MaiaraBookstore.Models
{
    public class Livro
    {
        public Livro() { }
        public Livro(string Titulo) 
        {
            this.Titulo = Titulo;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Título do Livro obrigatório")]
        public String Titulo { get; set; }
    }
}
