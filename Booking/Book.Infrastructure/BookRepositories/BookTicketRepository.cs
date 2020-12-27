using Book.Domain.Aggregates.BookingAggregate;
using Book.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Infrastructure.BookRepositories
{
    public class BookTicketRepository : IBookingRepository
    {
        private readonly BookingContext _context;

        public BookTicketRepository(BookingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(_context));
        }

        public IUnitOfWork UnitOfWork {
            get
            {
                return _context;
            }
        }


        public BookTicket Add(BookTicket ticket)
        {
            if (ticket.IsTransient())
                return _context.BookTickets.Add(ticket).Entity;
            return ticket;
        }

        public async Task<BookTicket> FindByIdAsync(string id)
        {
            var ticket = await _context.BookTickets.Where(x => x.TenantId == id).FirstOrDefaultAsync();
            return ticket;
        }

        public BookTicket Update(BookTicket ticket)
        {
            return _context.BookTickets
                 .Update(ticket)
                 .Entity;
        }
    }
}
