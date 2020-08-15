using Entities;

namespace Models.DTO
{
    public class LivroDTO : Livro
    {
        public LivroDTO() { }

        public LivroDTO(Livro livro)
        {
            this.Id = livro.Id;
            this.Titulo = livro.Titulo;
            this.Descricao = livro.Descricao;
        }
    }
}
