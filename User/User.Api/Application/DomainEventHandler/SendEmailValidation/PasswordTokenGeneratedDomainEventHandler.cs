using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.Event;
using User.Infrastructure.ExternalIntegrationService;
using User.Infrastructure.Models.Request;

namespace User.Api.Application.DomainEventHandler.SendEmailValidation
{
    public class PasswordTokenGeneratedDomainEventHandler : INotificationHandler<PasswordTokenGeneratedDomainEvent>
    {
        private IExternalService _externalService;

        public PasswordTokenGeneratedDomainEventHandler(IExternalService service)
        {
            _externalService = service;
        }
        public Task Handle(PasswordTokenGeneratedDomainEvent notification, CancellationToken cancellationToken)
        {
            string content = $"<h1>Your password verification code is: {notification.Token} </h1>";
            return _externalService.RequestSendEmailVerification(new EmailRequest(notification.EmailToSend,content));
        }
    }
}
