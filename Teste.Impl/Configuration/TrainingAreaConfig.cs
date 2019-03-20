using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Impl.Configuration
{
    public class TrainingAreaConfig : IEntityTypeConfiguration<TrainingArea>
    {
        public void Configure(EntityTypeBuilder<TrainingArea> entity)
        {
            entity.ToTable("TRAININGAREA");

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasColumnName("ID").UseSqlServerIdentityColumn().IsRequired();
            entity.Property(p => p.Name).HasColumnName("NAME").HasMaxLength(100).IsRequired();
            entity.Property(p => p.College).HasColumnName("COLLEGE").HasMaxLength(100).IsRequired();
            entity.Property(p => p.YearInit).HasColumnName("YEAR_INIT").IsRequired();
            entity.Property(p => p.YearFinish).HasColumnName("YEAR_FINISH").IsRequired();
        }
    }
}
