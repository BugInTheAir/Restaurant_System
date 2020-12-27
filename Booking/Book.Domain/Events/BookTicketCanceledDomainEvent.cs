using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Domain.Events
{
    public class BookTicketCanceledDomainEvent : INotification
    {
        public string BookerId { get; private set; }
        public string ResId { get; private set; }
        public string AtDate { get; private set; }

        public BookTicketCanceledDomainEvent(string bookerId, string resId, string atDate)
        {
            BookerId = bookerId;
            ResId = resId;
            AtDate = atDate;
        }
    }
}
