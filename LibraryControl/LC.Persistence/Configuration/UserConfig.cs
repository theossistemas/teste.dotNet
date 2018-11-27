using LC.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Persistence.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("TB_USERS");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();
            builder.Property(e => e.Name).HasColumnName("NAME").IsRequired();
            builder.Property(e => e.Login).HasColumnName("LOGIN").IsRequired();
            builder.Property(e => e.AcessKey).HasColumnName("ACESS_KEY").IsRequired();
            builder.HasIndex(e => e.AcessKey).IsUnique();
        }
    }
}