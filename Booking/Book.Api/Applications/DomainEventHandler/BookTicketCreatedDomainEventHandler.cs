using Book.Api.Applications.Queries;
using Book.Domain.Events;
using Book.Infrastructure.ExternalServices;
using Book.Infrastructure.Models.Common;
using Book.Infrastructure.Models.Request;
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
        private readonly IEmailService _emailService;
        private readonly IMailHtmlFactory _mailHtmlFactory;
        private readonly IResService _resService;
        private readonly IBookerQueries _bookerQueries;
        public BookTicketCreatedDomainEventHandler(IEmailService emailService, IMailHtmlFactory mailHtmlFactory, IResService resService, IBookerQueries bookerQueries)
        {
            _emailService = emailService;
            _mailHtmlFactory = mailHtmlFactory;
            _resService = resService;
            _bookerQueries = bookerQueries;
        }

        public async Task Handle(BookTicketCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var resInfo = await _resService.GetRestaurantInfo(notification.ResId);
            var request = new MailRequest
            (_mailHtmlFactory.GenerateBookingTicketCreatedEmail(notification.BookTicketId, notification.BookDate, resInfo.name, resInfo.address)
            , await _bookerQueries.GetBookerEmail(notification.BookerId));
            _emailService.SendEmailAsync(request);
        }
    }
}
