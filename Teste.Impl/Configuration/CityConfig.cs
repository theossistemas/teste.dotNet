using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Impl.Configuration
{
    public class CityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> entity)
        {
            entity.ToTable("CITY");

            entity.HasKey(a => a.Id);
            entity.Property(p => p.Id).HasColumnName("ID").UseSqlServerIdentityColumn().IsRequired();
            entity.Property(p => p.StateId).HasColumnName("STATE_ID").IsRequired();

            entity.Property(p => p.Name).HasColumnName("NAME").HasMaxLength(100).IsRequired();

            entity.HasOne<State>(e => e.State).WithMany().HasForeignKey(x => x.StateId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
