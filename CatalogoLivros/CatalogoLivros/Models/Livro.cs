using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CatalogoLivros.Models;

[Table("Livros")]
public class Livro
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(300)]
    public string Nome { get; set; }

    [Required]
    [StringLength(80)]
    public string Autor { get; set; }

    [Required]
    [RegularExpression(@"^\d{4}$", ErrorMessage = "O ano deve conter 4 dígitos.")]
    public int Ano { get; set; }

    [Required]
    [StringLength(80)]
    public string Editora { get; set; }

    public DateTime DataCadastro { get; set; }
    public int QtdEstoque { get; set; }
    public int GeneroId { get; set; }
    
    [JsonIgnore]
    public Genero? Genero { get; set; }

}
