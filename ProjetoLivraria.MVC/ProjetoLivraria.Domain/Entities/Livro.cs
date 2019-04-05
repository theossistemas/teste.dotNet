using System;

namespace ProjetoLivraria.Domain.Entities
{
   
    public class Livro
    {
        /// <summary>
        /// campo identificador do objeto
        /// </summary>
        public int IdLivro { get; set; }

        /// <summary>
        /// nome do livro
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// editora responsavel pelo livro
        /// </summary>
        public string Editora { get; set; }

        /// <summary>
        /// ano de publicacao
        /// </summary>
        public int Ano { get; set; }

        /// <summary>
        /// autor que escreveu o livro
        /// </summary>
        public string Autor { get; set; }

        /// <summary>
        /// categoria do livro. Ex: Romance, Auto-Ajuda, Tecnologia, Aventura, etc.
        /// </summary>
        public string Categoria  { get; set; }
}
}
