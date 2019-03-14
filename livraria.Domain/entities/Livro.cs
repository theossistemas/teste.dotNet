using livraria.Domain.entities.Validation;
using livraria.Domain.entities.Validation.common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace livraria.Domain.entities
{
    public class Livro : Validation<Livro>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LivroId { get; set; }
        public int AutorId { get; set; }
        public int CategoriaId { get; set; }
        public string Titulo { get; set; }
        public float Valor { get; set; }
        public Autor Autor { get; set; }
        public Categoria Categoria { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; set; } = new ValidationResult();

        [NotMapped]
        public bool IsValidCadastro
        {
            get
            {
                var fiscal = new LivroCadastroValidation();
                ValidationResult = fiscal.Valid(this);

                return ValidationResult.IsValid;
            }
        }

        [NotMapped]
        public bool IsValidAlteracao
        {
            get
            {
                var fiscal = new LivroAlteracaoValidation();
                ValidationResult = fiscal.Valid(this);

                return ValidationResult.IsValid;
            }
        }
    }
}
