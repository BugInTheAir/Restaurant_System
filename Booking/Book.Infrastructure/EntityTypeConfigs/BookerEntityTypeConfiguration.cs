using Book.Domain.Aggregates.BookerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Infrastructure.EntityTypeConfigs
{
    public class BookerEntityTypeConfiguration : IEntityTypeConfiguration<Booker>
    {
        public void Configure(EntityTypeBuilder<Booker> builder)
        {
            builder.ToTable("Booker", BookingContext.DEFFAULT_SCHEMA);
            builder.Ignore(b => b.DomainEvents);
            builder.HasKey(b => b.TenantId);
            builder.OwnsOne(b => b.BookerInf, bk =>
            {
                bk.WithOwner();
            }); 
            builder.Property(b => b.IsAnnonymous).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(b => b.TenantId).HasColumnName("BookerId").IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(b => b.Id).UseIdentityColumn().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        }
    }
}
