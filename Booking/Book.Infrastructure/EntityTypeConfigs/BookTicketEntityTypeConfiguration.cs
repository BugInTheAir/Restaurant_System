using Book.Domain.Aggregates.BookerAggregate;
using Book.Domain.Aggregates.BookingAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Infrastructure.EntityTypeConfigs
{
    public class BookTicketEntityTypeConfiguration : IEntityTypeConfiguration<BookTicket>
    {
        public void Configure(EntityTypeBuilder<BookTicket> builder)
        {
            builder.ToTable("BookTicket", BookingContext.DEFFAULT_SCHEMA);
            builder.Ignore(bt => bt.DomainEvents);
            builder.HasKey(bt => bt.TenantId);
            builder.Property(bt => bt.TenantId).HasColumnName("BookId").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(bt => bt.BookerId).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(bt => bt.CreatedDate).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(bt => bt.IsCanceled).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(bt => bt.IsFinished).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(bt => bt.ResId).IsRequired();
            builder.Property(bt => bt.CreatedDate).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.OwnsOne(bt => bt.BookInfo, bi =>
           {
               bi.WithOwner();
           });
            builder.HasOne<Booker>().WithMany().HasForeignKey(b => b.BookerId);
        }
    }
}
