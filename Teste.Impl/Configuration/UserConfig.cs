using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Impl.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("USER");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasColumnName("ID").UseSqlServerIdentityColumn().IsRequired();
            entity.Property(p => p.Login).HasColumnName("LOGIN").IsRequired();
            entity.Property(p => p.Password).HasColumnName("PASSWORD").HasMaxLength(50).IsRequired();
            entity.Property(p => p.ProfileId).HasColumnName("PROFILE_ID").HasMaxLength(50).IsRequired();


            entity.HasOne<Profile>(p => p.Profile).WithMany()
                .HasForeignKey(p => p.ProfileId).OnDelete(DeleteBehavior.Restrict);
        }

    }
}
