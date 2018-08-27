using Livraria.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livraria.Data.EFCore.Mapping
{
    public class LivroMap: EntidadeBaseMap<Livro>
    {
        public override void Configure(EntityTypeBuilder<Livro> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Nome);
            builder.Property(x => x.Descricao);
            builder.Property(x => x.Edicao);
            builder.HasOne(x => x.Autor).WithMany();
            builder.HasOne(x => x.Editora).WithMany();
        }
    }
}
