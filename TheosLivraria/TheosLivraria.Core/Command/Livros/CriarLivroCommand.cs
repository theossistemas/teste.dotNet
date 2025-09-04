using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheosLivraria.Core.Command.Livros
{
    public record CriarLivroCommand(string Titulo, string Autor, string Isbn, DateTime DataPublicacao, decimal Preco, int Estoque);
}
