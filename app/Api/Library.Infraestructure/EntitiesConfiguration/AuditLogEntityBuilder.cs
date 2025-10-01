using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infraestructure.EntitiesConfiguration;

public class AuditLogEntityBuilder : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AUDIT_LOGS");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ID");

        builder.Property(e => e.TableName).HasColumnName("TABLE_NAME");
        builder.Property(e => e.EventType).HasColumnName("EVENT_TYPE");
        builder.Property(e => e.UserName).HasColumnName("USER_NAME");
        builder.Property(e => e.Data).HasColumnName("DATA");
        builder.Ignore(x => x.Active);
        builder.Property(x => x.CreatedAt).HasColumnName("CREATED_AT").IsRequired();
    }
}