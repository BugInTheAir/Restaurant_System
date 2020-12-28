using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using User.Api.Application.Utils;
using User.Domain.Event;
using User.Infrastructure.ExternalIntegrationService;
using User.Infrastructure.Models.Request;

namespace User.Api.Application.DomainEventHandler
{
    public class ValidateEmailDomainEventHandler : INotificationHandler<ValidateEmailDomainEvent>
    {
        private IExternalService _externalService;

        public ValidateEmailDomainEventHandler(IExternalService service)
        {
            _externalService = service;
        }
        public Task Handle(ValidateEmailDomainEvent notification, CancellationToken cancellationToken)
        {
            return _externalService.RequestSendEmailVerification(new EmailRequest(notification.EmailToValidate, notification.ContentToSend));
        }
    }
}
