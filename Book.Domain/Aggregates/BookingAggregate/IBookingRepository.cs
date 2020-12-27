using Book.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.Aggregates.BookingAggregate
{
    public interface IBookingRepository : IRepository<BookTicket>
    {
        BookTicket Add(BookTicket ticket);
        Task<BookTicket> FindByIdAsync(string Id);
        BookTicket Update(BookTicket ticket);
    }
}
