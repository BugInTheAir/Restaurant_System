using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Aggregates.RestaurantAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Infrastructure.EntityTypeConfigs
{
    public class RestaurantEntityTypeConfiguration : IEntityTypeConfiguration<Restaurant.Domain.Aggregates.RestaurantAggregate.Restaurant>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Restaurant.Domain.Aggregates.RestaurantAggregate.Restaurant> builder)
        {
            builder.ToTable("Restaurant", RestaurantContext.DEFFAULT_SCHEMA);
            builder.HasKey(r => r.TenantId);
            builder.Ignore(r => r.DomainEvents);
            builder.Property<int>("Id").UseIdentityColumn();
            builder.OwnsOne(r => r.WorkTime, t =>
            {
                t.WithOwner();
            });
            builder.OwnsOne(r => r.Address, a =>
            {
                a.WithOwner();
            });
            builder.Property(r => r.Seats).IsRequired();
            builder.Property(r => r.Name).IsRequired();
            builder.Property(r => r.TenantId).IsRequired().HasColumnName("ResId");
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasOne(x => x.RestaurantType).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(r => r.RestaurantAndMenus).WithOne().HasForeignKey(x=>x.ResId).OnDelete(DeleteBehavior.Cascade);
            var navigation = builder.Metadata.FindNavigation(nameof(Restaurant.Domain.Aggregates.RestaurantAggregate.Restaurant.RestaurantAndMenus));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
