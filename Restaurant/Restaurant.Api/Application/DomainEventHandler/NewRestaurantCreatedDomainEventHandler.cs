using MediatR;
using Restaurant.Domain.Events;
using Restaurant.Infrastructure.ExternalServices;
using Restaurant.Infrastructure.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.DomainEventHandler
{
    public class NewRestaurantCreatedDomainEventHandler : INotificationHandler<NewRestaurantCreatedDomainEvent>
    {
        private readonly IEmailService _emailService;

        public NewRestaurantCreatedDomainEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public Task Handle(NewRestaurantCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            string contentSend = $"New restaurant name: {notification.RestaurantName} has just opened from {notification.WorkTime.OpenTime} to {notification.WorkTime.CloseTime}  at {notification.Address.Street},{notification.Address.Ward}, {notification.Address.District}," +
                $"our phone number: {notification.Phone}";

            throw new NotImplementedException();
        }
    }
}
