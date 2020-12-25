using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Aggregates.RestaurantAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Infrastructure.EntityTypeConfigs
{
    public class RestaurantTypeEntityTypeConfiguration : IEntityTypeConfiguration<RestaurantType>
    {
        public void Configure(EntityTypeBuilder<RestaurantType> builder)
        {
            builder.ToTable("RestaurantType",RestaurantContext.DEFFAULT_SCHEMA);
            builder.HasKey(x => x.TenantId);
            builder.Ignore(x => x.DomainEvents);
            builder.Property<int>("Id").UseIdentityColumn();
            builder.HasIndex(x => x.TypeName).IsUnique();
            builder.Property(x => x.TypeName).IsRequired();
            builder.Property(x => x.TenantId).IsRequired().HasColumnName("ResTypeId").IsRequired();
        }
    }
}
