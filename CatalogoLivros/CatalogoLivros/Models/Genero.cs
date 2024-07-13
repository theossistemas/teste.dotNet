using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoLivros.Models;

[Table("Generos")]
public class Genero
{
    public Genero()
    {
        Livros = new Collection<Livro>();
    }

    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(80)]
    public string Nome { get; set; }

    public ICollection<Livro>? Livros { get; set; }
}
