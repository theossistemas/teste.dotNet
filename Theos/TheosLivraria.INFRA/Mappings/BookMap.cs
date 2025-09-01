
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheosLivraria.Domain.Models.Entities;

namespace TheosLivraria.INFRA.Mappings
{
    public class BookMap : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.ToTable("Book", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.Author)
                .HasColumnType("varchar")
                     .IsRequired()
                     .HasMaxLength(200);

            builder.Property(x => x.CreatedDate)
                    .HasColumnType("datetime")
                     .IsRequired();

            builder.Property(x => x.ModifiedDate)
                    .HasColumnType("datetime")
                     .IsRequired();

            builder.HasOne(x => x.Status)
                   .WithMany()
                   .HasForeignKey(x => x.StatusId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Active)
               .HasColumnType("bit")
                  .IsRequired();
        }
    }
}
