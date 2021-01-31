using System;
using System.ComponentModel.DataAnnotations;

namespace MaiaraBookstore.Models
{
    public class LogBookstore
    {
        public LogBookstore() { }
        public LogBookstore(string Descricao) 
        {
            LogDescricao = Descricao;
            DataDeRegistro = DateTime.Now;
        }
        public LogBookstore(string logDescricao, Livro livro) 
        {
            LogDescricao = logDescricao;
            DataDeRegistro = DateTime.Now;
            LivroId = livro.Id;
        }

        [Key]
        public int Id { get; set; }

        public string LogDescricao { get; set; }

        public DateTime DataDeRegistro { get; set; }
        
        public int LivroId { get; set; }
    }
}
