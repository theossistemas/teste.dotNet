using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class LivroLogs : ILivrosLogs
    {
        #region Properties
        public string NomeLivro { get; set; }
        public DateTime Data { get; set; }
        public string Acao { get; set; }
        #endregion

        #region Method
        public LivroLogs ToLivroLog(Livros livro, string action)
        {
            return new LivroLogs
            {
                NomeLivro = livro.NomeLivro,
                Data = DateTime.Now,
                Acao = action,
            };
        }
        #endregion
    }
}
