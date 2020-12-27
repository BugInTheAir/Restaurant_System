using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Aggregates.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Infrastructure.EntityTypeConfigs
{
    class RestaurantAndMenuEntityTypeConfiguration : IEntityTypeConfiguration<RestaurantAndMenu>
    {
        public void Configure(EntityTypeBuilder<RestaurantAndMenu> builder)
        {
            builder.ToTable("ResAndMenu",RestaurantContext.DEFFAULT_SCHEMA);
            builder.HasKey(x => new { x.MenuId, x.ResId, x.TenantId });
            builder.Ignore(x => x.DomainEvents);
            builder.Property<int>("Id").UseIdentityColumn().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(x => x.MenuId).IsRequired();
            builder.Property(x => x.ResId).IsRequired();
            builder.Property(x => x.TenantId).HasColumnName("ResAndMenuId").IsRequired();
        }
    }
}
