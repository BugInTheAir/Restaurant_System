using Book.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Book.Api.Applications.DomainEventHandler
{
    public class BookTicketCreatedDomainEventHandler : INotificationHandler<BookTicketCreatedDomainEvent>
    {
        public Task Handle(BookTicketCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
