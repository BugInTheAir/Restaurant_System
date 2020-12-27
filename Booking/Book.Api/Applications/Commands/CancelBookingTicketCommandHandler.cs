using Book.Domain.Aggregates.BookingAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Book.Api.Applications.Commands
{
    public class CancelBookingTicketCommandHandler : IRequestHandler<CancelBookingTicketCommand, bool>
    {
        private readonly IBookingRepository _bookingRepository;

        public CancelBookingTicketCommandHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(_bookingRepository));
        }

        public async Task<bool> Handle(CancelBookingTicketCommand request, CancellationToken cancellationToken)
        {
            var existedBooking = await _bookingRepository.FindByIdAsync(request.BookId);
            if (existedBooking == null)
                throw new KeyNotFoundException();
            existedBooking.CancelBooking();
            _bookingRepository.Update(existedBooking);
            await _bookingRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}
