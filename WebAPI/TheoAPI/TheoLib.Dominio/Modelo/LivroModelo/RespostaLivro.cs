using System;

namespace TheoLib.Dominio.Modelo.LivroModelo
{
    public class RespostaLivro
    {
        public long Id { get; set; } 
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Autor { get; set; }
        public DateTime DataPublicacao { get; set; }   
        public long Estoque { get; set; }
        public bool Ativo { get; set; }
        public int IdUsuario { get; set; }
    }
}
