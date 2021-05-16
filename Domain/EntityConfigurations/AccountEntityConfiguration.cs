using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain
{
    public class AccountEntityConfiguration : BaseEntityConfiguration<Account>, IEntityMapping
    {
        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.PasswordHash).IsRequired();
            builder.Property(e => e.Role).IsRequired().HasDefaultValue(Role.User);
            builder.Property(e => e.VerificationToken);
            builder.Property(e => e.Verified).IsRequired(false).HasDefaultValue(null);
            builder.Ignore(e => e.IsVerified);
            builder.Property(e => e.ResetToken);
            builder.Property(e => e.ResetTokenExpires).IsRequired(false).HasDefaultValue(null);
            builder.Property(e => e.PasswordReset).IsRequired(false).HasDefaultValue(null);
            builder.Ignore(e => e.Password);
            builder.Property(e => e.RegisterComplete).IsRequired().HasDefaultValue(false);

            builder.HasMany(e => e.RefreshTokens)
                .WithOne(e => e.Account)
                .HasForeignKey(e => e.AccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
