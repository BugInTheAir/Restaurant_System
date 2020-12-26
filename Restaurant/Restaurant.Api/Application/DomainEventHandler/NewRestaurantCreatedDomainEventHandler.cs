using MediatR;
using Restaurant.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.DomainEventHandler
{
    public class NewRestaurantCreatedDomainEventHandler : INotificationHandler<NewRestaurantCreatedDomainEvent>
    {
        public Task Handle(NewRestaurantCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
