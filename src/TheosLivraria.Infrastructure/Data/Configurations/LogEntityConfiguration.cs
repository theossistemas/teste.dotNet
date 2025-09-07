using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheosLivraria.Infrastructure.Data.Configurations;

public class LogEntryEntityConfiguration : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.ToTable("Logs");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Level)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(l => l.Message)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(l => l.RequestPath)
            .HasMaxLength(500);

        builder.Property(l => l.Method)
            .HasMaxLength(10);

        builder.Property(l => l.RequestBody)
            .HasColumnType("nvarchar(max)");

        builder.Property(l => l.ResponseBody)
            .HasColumnType("nvarchar(max)");

        builder.Property(l => l.StatusCode);

        builder.Property(l => l.Exception)
            .HasColumnType("nvarchar(max)");

        builder.Property(l => l.UserId);

        builder.HasOne(l => l.User)
            .WithMany()
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(l => l.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(l => l.UpdatedAt);
    }
}
