using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Domain.Events
{
    public class BookTicketCreatedDomainEvent : INotification
    {
        public string ResId { get; private set; }
        public string BookDate { get; private set; }
        public string BookTicketId { get; private set; }
        public string BookerId { get; private set; }
        public BookTicketCreatedDomainEvent(string resId, string bookDate, string ticketId, string bookerId)
        {
            ResId = resId;
            BookDate = bookDate;
            BookTicketId = ticketId;
            BookerId = bookerId;
        }
    }
}
