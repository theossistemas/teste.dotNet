
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheosLivraria.Domain.Models.Entities;

namespace TheosLivraria.INFRA.Mappings
{
    public class AuditLogsMap : IEntityTypeConfiguration<AuditLogEntity>
    {
        public void Configure(EntityTypeBuilder<AuditLogEntity> builder)
        {
            builder.ToTable("AuditLogs", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.CorrelationId)
           .HasColumnType("varchar(50)")
           .HasMaxLength(50);

            builder.Property(x => x.Timestamp)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(x => x.Level)
                .HasColumnType("varchar(20)")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Message)
                .HasColumnType("varchar(500)")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.Path)
                .HasColumnType("varchar(250)")
                .HasMaxLength(250);

            builder.Property(x => x.Data)
                .HasColumnType("varchar(max)")
                .IsRequired(false);

            builder.Property(x => x.Active)
               .HasColumnType("bit")
                  .IsRequired();

            builder.Property(x => x.CreatedDate)
                    .HasColumnType("datetime")
                     .IsRequired();

            builder.Property(x => x.ModifiedDate)
                    .HasColumnType("datetime")
                     .IsRequired();
        }
    }
}
