using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Impl.Configuration
{
    public class StateConfig : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> entity)
        {
            entity.ToTable("STATE");

            entity.HasKey(p => p.Id);
       
            entity.Property(p => p.Id).HasColumnName("ID").IsRequired().ValueGeneratedNever();
            entity.Property(p => p.Name).HasColumnName("NAME").HasMaxLength(100).IsRequired();

            entity.Property(p => p.Uf).HasColumnName("UF").HasMaxLength(2).IsRequired();

        }
    }
}
