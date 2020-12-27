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
        public string BookerId { get; private set; }

        public BookTicketCreatedDomainEvent(string resId, string bookDate, string bookerId)
        {
            ResId = resId;
            BookDate = bookDate;
            BookerId = bookerId;
        }
    }
}
