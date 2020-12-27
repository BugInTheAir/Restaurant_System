using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Aggregates.FoodAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Infrastructure.EntityTypeConfigs
{
    public class FoodEntityTypeConfiguration : IEntityTypeConfiguration<FoodItem>
    {
        public void Configure(EntityTypeBuilder<FoodItem> builder)
        {
            builder.ToTable("FoodItem", RestaurantContext.DEFFAULT_SCHEMA);
            builder.HasKey(x => x.TenantId);
            builder.Property<int>("Id").UseIdentityColumn().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(x => x.TenantId).HasColumnName("FoodId").IsRequired();
            builder.Ignore(x => x.DomainEvents);
            builder.OwnsOne(x => x.FoodInfo, fi =>
              {
                  fi.WithOwner();
              });
            builder.HasMany(x => x.FoodAndMenus).WithOne().HasForeignKey(x => x.FoodId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
