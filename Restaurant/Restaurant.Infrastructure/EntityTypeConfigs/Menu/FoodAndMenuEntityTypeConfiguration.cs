using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Aggregates.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Infrastructure.EntityTypeConfigs
{
    public class FoodAndMenuEntityTypeConfiguration : IEntityTypeConfiguration<FoodAndMenu>
    {
        public void Configure(EntityTypeBuilder<FoodAndMenu> builder)
        {
            builder.ToTable("FoodAndMenu",RestaurantContext.DEFFAULT_SCHEMA);
            builder.HasKey(x => new { x.TenantId, x.MenuId, x.FoodId });
            builder.Property<int>("Id").UseIdentityColumn().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Ignore(x => x.DomainEvents);
            builder.Property(x => x.FoodId).IsRequired();
            builder.Property(x => x.TenantId).HasColumnName("FoodAndMenuId").IsRequired();
            builder.Property(x => x.MenuId).IsRequired();
        }
    }
}
