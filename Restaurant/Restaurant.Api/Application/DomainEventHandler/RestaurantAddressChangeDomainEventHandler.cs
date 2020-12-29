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
    public class RestaurantAddressChangeDomainEventHandler : INotificationHandler<RestaurantAddressChangedDomainEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public RestaurantAddressChangeDomainEventHandler(IEmailService emailService, IUserService userService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        public Task Handle(RestaurantAddressChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            return _userService.GetEmails().ContinueWith(x =>
            {
                string contentToSend = $"Dear customers, we have to informed you that our restaurant: {notification.RestaurantName} has changed address from {notification.ToAddress(notification.OldAddress)} to {notification.ToAddress(notification.NewAddress)}";
                for (int i =0; i < x.Result.Count; i++)
                {
                    _emailService.SendMail(new EmailRequest(x.Result[i], contentToSend));
                }
            });
        }
    }
}
