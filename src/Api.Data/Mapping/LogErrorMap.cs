using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class LogErrorMap : IEntityTypeConfiguration<LogErrorEntity>
    {
        public void Configure(EntityTypeBuilder<LogErrorEntity> builder)
        {
            builder.ToTable("LogErrors");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.CreatedAt);
            builder.Property(u => u.UpdatedAt);
            builder.Property(u => u.Message);
            builder.Property(u => u.User);
        }
    }
}
