using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Impl.Configuration
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> entity)
        {
            entity.ToTable("BOOK");


            entity.HasKey(b => b.Id);
            entity.Property(b => b.Id).HasColumnName("ID").UseSqlServerIdentityColumn().IsRequired();
            entity.Property(b => b.Title).HasColumnName("TITLE").HasMaxLength(100).IsRequired();
            entity.Property(b => b.Pages).HasColumnName("PAGES").IsRequired();
            entity.Property(b => b.PublishingCompany).HasColumnName("PUBLISHING_COMPANY").IsRequired();
            entity.Property(b => b.Edition).HasColumnName("EDTION").IsRequired();
            entity.Property(b => b.Url).HasColumnName("URL").HasMaxLength(200).IsRequired();
        }
    }
}
