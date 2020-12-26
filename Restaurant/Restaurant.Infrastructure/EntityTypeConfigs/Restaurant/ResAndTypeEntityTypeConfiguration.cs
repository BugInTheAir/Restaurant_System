using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Aggregates.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Infrastructure.EntityTypeConfigs.Restaurant
{
    public class ResAndTypeEntityTypeConfiguration : IEntityTypeConfiguration<ResAndType>
    {
        public void Configure(EntityTypeBuilder<ResAndType> builder)
        {
            builder.ToTable("ResAndType", RestaurantContext.DEFFAULT_SCHEMA);
            builder.HasKey(x => new { x.TenantId, x.ResId, x.ResTypeId });
            builder.Ignore(x => x.DomainEvents);
            builder.Property(x => x.TenantId).HasColumnName("ResAndTypeId").IsRequired();
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ResTypeId).IsRequired();
            builder.Property(x => x.ResId).IsRequired();
        }
    }
}
