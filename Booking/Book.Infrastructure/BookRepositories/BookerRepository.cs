using Book.Domain.Aggregates;
using Book.Domain.Aggregates.BookerAggregate;
using Book.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Infrastructure.BookRepositories
{
    public class BookerRepository : IBookerRepository
    {
        private readonly BookingContext _context;

        public BookerRepository(BookingContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork {
            get { return _context; }
        }

        public Booker Add(Booker booker)
        {
            if (booker.IsTransient())
                return _context.Add(booker).Entity;
            return booker;
        }

        public async  Task<IEnumerable<Booker>> FindAllByNameAsync(string name)
        {
            return await _context.Bookers.Where(x => x.BookerInf.UserName == name).ToListAsync();
        }

        public async Task<Booker> FindByNameAsync(string name)
        {
            var booker = await _context.Bookers.Where(x => x.BookerInf.UserName == name && !x.IsAnnonymous).FirstOrDefaultAsync();
            return booker;
        }
    }
}
