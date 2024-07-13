using CatalogoLivros.Models;
using System.ComponentModel.DataAnnotations;

namespace CatalogoLivros.DTOs;

public class GeneroDTO
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(80)]
    public string Nome { get; set; }
    public ICollection<Livro>? Livros { get; set; }
}
