using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class RefreshTokenEntityConfiguration : BaseEntityConfiguration<RefreshToken>, IEntityMapping
    {
        public override void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.CreatedByIp);
            builder.Property(e => e.Expires);
            builder.Ignore(e => e.IsExpired);
            builder.Ignore(e => e.IsActive);
            builder.Property(e => e.ReplacedByToken);
            builder.Property(e => e.Revoked);
            builder.Property(e => e.RevokedByIp);
            builder.Property(e => e.Token);

            builder.HasOne(e => e.Account)
                .WithMany(e => e.RefreshTokens)
                .HasForeignKey(e => e.AccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
