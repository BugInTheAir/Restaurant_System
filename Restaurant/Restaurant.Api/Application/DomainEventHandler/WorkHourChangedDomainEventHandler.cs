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
    public class WorkHourChangedDomainEventHandler : INotificationHandler<WorkHourChangedDomainEvent>
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public WorkHourChangedDomainEventHandler(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        public Task Handle(WorkHourChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            return _userService.GetEmails().ContinueWith(x =>
            {
                string contentToSend = $"Dear customers, we have to informed you that our restaurant: {notification.RestaurantName} at address: {notification.Address.Street}, {notification.Address.Ward}, {notification.Address.District} has changed work hour, our new open time is: {notification.ChangedTime.OpenTime} and close time is to {notification.ChangedTime.CloseTime}";
                for (int i = 0; i < x.Result.Count; i++)
                {
                    _emailService.SendMail(new EmailRequest(x.Result[i], contentToSend));
                }
            });
        }
    }
}
