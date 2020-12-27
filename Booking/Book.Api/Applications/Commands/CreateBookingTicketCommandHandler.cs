using Book.Domain.Aggregates;
using Book.Domain.Aggregates.BookerAggregate;
using Book.Domain.Aggregates.BookingAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Book.Api.Applications.Commands
{
    public class CreateBookingTicketCommandHandler : IRequestHandler<CreateBookingTicketCommand, bool>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookerRepository _bookerRepository;

        public CreateBookingTicketCommandHandler(IBookingRepository bookingRepository, IBookerRepository bookerRepository)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(_bookingRepository));
            _bookerRepository = bookerRepository ?? throw new ArgumentNullException(nameof(_bookerRepository));
        }

        public async Task<bool> Handle(CreateBookingTicketCommand request, CancellationToken cancellationToken)
        {
            BookerInfo info = new BookerInfo(request.UserName, request.Email, request.Phone);
            var existedBookers = await _bookerRepository.FindAllByNameAsync(request.UserName);
            var existedBooker = existedBookers.Where(x =>
            {
                if (x.BookerInf.Equals(info))
                    return true;
                return false;
            }).FirstOrDefault();
            if (request.IsAnnonymous)
            {
                if (existedBooker == null)
                {
                    existedBooker = new Booker(request.UserName, request.Email, request.Phone, request.Note);
                    existedBooker = existedBooker.GetAnnonymousBooker();
                    _bookerRepository.Add(existedBooker);
                }
            }
            else
            {
                if (existedBooker == null)
                {
                    existedBooker = new Booker(request.UserName, request.Email, request.Phone, request.Note);
                    existedBooker = existedBooker.GetUserBooker();
                    _bookerRepository.Add(existedBooker);
                }
            }
            BookTicket ticket = new BookTicket(existedBooker.TenantId, request.ResId,new BookTicketInfo(request.BookDate, request.AtHour, request.AtMinute, request.Note));
            _bookingRepository.Add(ticket);
            await _bookingRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}
