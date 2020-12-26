using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Aggregates.RestaurantAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Infrastructure.EntityTypeConfigs
{
    public class RestaurantEntityTypeConfiguration : IEntityTypeConfiguration<Restaurants>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Restaurants> builder)
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
            builder.Property(x => x.Phone).IsRequired();

            builder.HasMany(r => r.ResImages).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(r => r.RestaurantAndMenus).WithOne().HasForeignKey(x => x.ResId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(r => r.ResAndTypes).WithOne().HasForeignKey(rt => rt.ResId).OnDelete(DeleteBehavior.Cascade);
            var imagesNavigation = builder.Metadata.FindNavigation(nameof(Restaurants.ResImages));
            imagesNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            var navigation = builder.Metadata.FindNavigation(nameof(Restaurants.RestaurantAndMenus));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            var resTypeNavigation = builder.Metadata.FindNavigation(nameof(Restaurants.ResAndTypes));
            resTypeNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
