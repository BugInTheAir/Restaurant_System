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
        private readonly IUserService _userService;
        public NewRestaurantCreatedDomainEventHandler(IEmailService emailService, IUserService userService)
        {
            _emailService = emailService;
            _userService = userService;
        }

        public async Task Handle(NewRestaurantCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            string contentSend = $"Dear customers, we have to inform to you about our new restaurant {notification.RestaurantName} has just opened from {notification.WorkTime.OpenTime} to {notification.WorkTime.CloseTime}  at {notification.Address.Street},{notification.Address.Ward}, {notification.Address.District}," +
                $"our phone number: {notification.Phone}";
            var emailToSend = await _userService.GetEmails();
            emailToSend.ForEach(x => _emailService.SendMail(new EmailRequest(x, contentSend)));
        }
    }
}
