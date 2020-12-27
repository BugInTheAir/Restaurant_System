using Book.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.Aggregates.BookerAggregate
{
    interface IBookerRepository : IRepository<Booker>
    {
        Booker Add(Booker booker);
        Task<Booker> FindBookerByName(string name);
        Task<Booker> FindBookerById(string id);
    }
}
