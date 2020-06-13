using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMM.Library.Domain.Models;

namespace MMM.Library.Infra.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Code)
                 .HasDefaultValueSql("NEXT VALUE FOR CategoryCode");

            builder.Property(p => p.CategoryName)
                .IsRequired()
                .HasColumnType("varchar(50)");

            // Query Filter for Soft Delete
            builder.HasQueryFilter(p => !p.IsDeleted);

            // 1 : N -> EF Relationship
            builder.HasMany(category => category.Books)
               .WithOne(book => book.Category)
               .HasForeignKey(book => book.CategoryId);

            builder.ToTable("Categories");
        }
    }
}
