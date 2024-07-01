using LivrariaJc.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace LivrariaJc.Data.Seeds
{
    public class LivroSeeds
    {
        public static void Livros(EntityTypeBuilder<LivrosEntidade> builder)
        {
            builder.HasData(new List<LivrosEntidade>
            {
                new LivrosEntidade()
                {
                    Id = 1,
                    Autor = "William Shakespeare",
                    Titulo = "Romeu e Julieta",
                    DataLancamento = new DateTime(1875, 7, 12, 0,0,0),
                    Valor = 160.00M,
                },
                new LivrosEntidade()
                {
                    Id = 2,
                    Autor = "Pero Vaz de Caminha",
                    Titulo = "Carta de Pero Vaz de Caminha",
                    DataLancamento = new DateTime(1842, 10, 11, 0,0,0),
                    Valor = 100.00M,
                },
                new LivrosEntidade()
                {
                    Id = 3,
                    Autor = "Afonso Henriques de Lima Barreto",
                    Titulo = "Triste Fim de Policarpo Quaresma: análise, contexto histórico e mais",
                    DataLancamento = new DateTime(1985, 1, 25, 0,0,0),
                    Valor = 50.00M,
                },

                new LivrosEntidade()
                {
                    Id = 4,
                    Autor = "Gil Vicente",
                    Titulo = "Auto da Barca do Inferno",
                    DataLancamento = new DateTime(2001, 4, 10, 0,0,0),
                    Valor = 40.00M,
                }
            });
        }
    }
}
