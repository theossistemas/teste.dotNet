using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public abstract class BaseEntityConfiguration<TDomain> : IEntityTypeConfiguration<TDomain> where TDomain : BaseDomain
    {
        public virtual void Configure(EntityTypeBuilder<TDomain> builder)
        {
            builder.HasIndex(e => e.ID);

            builder.Property(e => e.ID).ValueGeneratedOnAdd();

            builder.Property(e => e.Created);
            builder.Property(e => e.Updated);
            builder.Property(e => e.Active).HasDefaultValue(true);
        }
    }
}
