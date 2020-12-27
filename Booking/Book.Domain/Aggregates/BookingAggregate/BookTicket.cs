using Book.Domain.Events;
using Book.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Domain.Aggregates.BookingAggregate
{
    public class BookTicket : Entity, IAggregateRoot
    {
        public string TenantId { get; private set; }
        public string ResId { get; private set; }
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
            IsCanceled = false;
        }

        public BookTicket(string bookerId, string resId, BookTicketInfo bookInfo) : this()
        {
            BookerId = bookerId;
            BookInfo = bookInfo;
            ResId = resId;
            AddDomainEvent(new BookTicketCreatedDomainEvent(ResId, $"{ConvertTimeToString(BookInfo.AtHour)}:{ConvertTimeToString(BookInfo.AtMinute)} {BookInfo.AtDate}", BookerId));
        }
        public void FinishBooking()
        {
            this.IsFinished = true;
        }
        public void CancelBooking()
        {
            this.IsCanceled = true;
            AddDomainEvent(new BookTicketCanceledDomainEvent(BookerId, ResId, $"{ConvertTimeToString(BookInfo.AtHour)}:{ConvertTimeToString(BookInfo.AtMinute)} {BookInfo.AtDate}"));
        }
        private string ConvertTimeToString(int time)
        {
            if (time > 10)
                return time.ToString();
            return "0" + time.ToString();
        }
    }
}
