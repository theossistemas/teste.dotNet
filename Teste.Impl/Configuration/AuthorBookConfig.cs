using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Impl.Configuration
{
    public class AuthorBookConfig : IEntityTypeConfiguration<AuthorBook>
    {
        public void Configure(EntityTypeBuilder<AuthorBook> entity)
        {
            entity.ToTable("AUTHORBOOK");

            entity.HasKey(a => new { a.Id});

            //entity.HasKey(a => new { a.Id, a.BookId, a.PersonId });

            entity.Property(a => a.Id).HasColumnName("Id").IsRequired();

            entity.Property(a => a.PersonId).HasColumnName("CODPERSON").IsRequired();

            entity.Property(a => a.BookId).HasColumnName("CODBOOK").IsRequired();

            entity.Property(a => a.YearPublication).HasColumnName("YEAR_PUBLICATION").IsRequired();

            entity.HasOne(p => p.Person).WithMany(c => c.AuthorBooks).HasForeignKey(c => c.PersonId);
            entity.HasOne(b => b.Book).WithMany(c => c.AuthorBooks).HasForeignKey(c => c.BookId);
        }
    }
}
