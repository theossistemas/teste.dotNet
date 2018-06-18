using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema
{
    public class Paginacao<T> :  List<T>
    {
      public int PaginaInicial { get; private set; }
      public int TotalPaginas { get; private set; }
      public int TotalR { get; private set; }

      public Paginacao(List<T> itens, int qtd, int paginaInicial, int totalPaginas)
      {
         PaginaInicial = paginaInicial;
         TotalPaginas = (int)Math.Ceiling(qtd / (double)totalPaginas);

         TotalR = qtd;

         this.AddRange(itens);
      }

      public bool PaginaAnterior
      {
         get
         {
            return (PaginaInicial > 1);
         }
      }

      public bool ProximaPagina
      {
         get
         {
            return (PaginaInicial < TotalPaginas);
         }
      }

      public static async Task<Paginacao<T>> CreateAsync(IQueryable<T> source, int paginaInicial, int totalPaginas)
      {
         var qtd = await source.CountAsync();
         var itens = await source.Skip((paginaInicial - 1) * totalPaginas).Take(totalPaginas).ToListAsync();
         return new Paginacao<T>(itens, qtd, paginaInicial, totalPaginas);
      }
   }
}
