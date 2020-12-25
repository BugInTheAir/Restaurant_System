using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Aggregates.Common;
using Restaurant.Domain.Aggregates.MenuAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Infrastructure.EntityTypeConfigs
{
    public class MenuEntityTypeConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menu", RestaurantContext.DEFFAULT_SCHEMA);
            builder.Ignore(x => x.DomainEvents);
            builder.HasKey(x => x.TenantId);
            builder.Property(x => x.TenantId).HasColumnName("MenuId").IsRequired();
            builder.Property<int>("Id").UseIdentityColumn();
            builder.OwnsOne(x => x.MenuInfo, mi =>
            {
                mi.WithOwner();
            });
            builder.HasMany(x => x.RestaurantAndMenus).WithOne().HasForeignKey(y => y.MenuId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.FoodAndMenus).WithOne().HasForeignKey(y => y.MenuId).OnDelete(DeleteBehavior.Cascade);
           
            var navigation = builder.Metadata.FindNavigation(nameof(Menu.FoodAndMenus));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
