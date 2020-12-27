using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Api.Applications.Commands
{
    public class CancelBookingTicketCommand : IRequest<bool>
    {
        public string BookId { get; private set; }

        public CancelBookingTicketCommand(string bookId)
        {
            BookId = bookId;
        }
    }
}
