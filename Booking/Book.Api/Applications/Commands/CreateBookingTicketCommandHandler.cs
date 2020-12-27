using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Api.Applications.Commands
{
    public class CreateBookingTicketCommandHandler : IRequest<bool>
    {
        public string UserName { get; private set; }
        public string BookDate { get; private set; }

    }
}
