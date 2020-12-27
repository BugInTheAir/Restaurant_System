using Book.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Domain.Aggregates.BookingAggregate
{
    public class BookTicket : Entity, IAggregateRoot
    {
        public string TenantId { get; private set; }
        public string BookerId { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public BookTicketInfo BookInfo { get; private set; }
        public bool IsFinished { get; private set; }
        public bool IsCanceled { get; private set; }
        protected BookTicket()
        {
            this.TenantId = $"BO-{DateTime.Now.ToShortDateString()}-{Guid.NewGuid().ToString().Split('-')[0]}";
            CreatedDate = DateTime.Now;
            IsFinished = false;
        }

        public BookTicket(string tenantId, string bookerId, DateTime createdDate, BookTicketInfo bookInfo):this()
        {
            TenantId = tenantId;
            BookerId = bookerId;
            CreatedDate = createdDate;
            BookInfo = bookInfo;
        }
        public void FinishBooking()
        {
            this.IsFinished = true;
        }
        public void CancelBooking()
        {
            this.IsCanceled = true;
            
        }
    }
}
