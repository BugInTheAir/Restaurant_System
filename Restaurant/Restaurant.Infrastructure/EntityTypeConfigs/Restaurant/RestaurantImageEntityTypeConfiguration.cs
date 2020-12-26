using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Aggregates.RestaurantAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Infrastructure.EntityTypeConfigs.Restaurant
{
    public class RestaurantImageEntityTypeConfiguration : IEntityTypeConfiguration<ResImage>
    {
        public void Configure(EntityTypeBuilder<ResImage> builder)
        {
            builder.ToTable("ResImages", RestaurantContext.DEFFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseHiLo("resimgseq", RestaurantContext.DEFFAULT_SCHEMA);
            builder.Ignore(x => x.DomainEvents);
            builder.Property(x => x.ImageUrl).IsRequired();
        }
    }
}
