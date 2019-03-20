using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Impl.Configuration
{
    public class ProfessionConfig : IEntityTypeConfiguration<Profession>
    {
        public void Configure(EntityTypeBuilder<Profession> entity)
        {
            entity.ToTable("PROFESSION");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasColumnName("ID").UseSqlServerIdentityColumn().IsRequired();
            entity.Property(p => p.Name).HasColumnName("NAME").HasMaxLength(100).IsRequired();
        }
    }
}
