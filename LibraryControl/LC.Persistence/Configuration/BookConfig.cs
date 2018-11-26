using LC.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Persistence.Configuration
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("TB_BOOKS");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();
            builder.Property(e => e.CreatdAt).HasColumnName("CREATED_AT").IsRequired();
            builder.Property(e => e.Price).HasColumnName("PRICE").HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(e => e.Name).HasColumnName("NAME").IsRequired();
            builder.Property(e => e.DescriptionShort).HasColumnName("DESCRIPTION_SHORT");
            builder.Property(e => e.DescriptionLong).HasColumnName("DESCRIPTION_LONG");
            builder.Property(e => e.Photo).HasColumnName("PHOTO").IsRequired();
            builder.Property(e => e.Author).HasColumnName("AUTHOR").IsRequired();
            builder.Property(e => e.Year).HasColumnName("YEAR").IsRequired();
            builder.Property(e => e.Language).HasColumnName("LANGUAGE");
            builder.Property(e => e.Publishing).HasColumnName("PUBLISHING").IsRequired();
            builder.Property(e => e.Weight).HasColumnName("WEIGHT");
            builder.Property(e => e.QuantityPages).HasColumnName("QUANTITY_PAGES");
            builder.Property(e => e.Slug).HasColumnName("SLUG").IsRequired();
            builder.HasIndex(e => e.Slug).IsUnique();
        }
    }
}