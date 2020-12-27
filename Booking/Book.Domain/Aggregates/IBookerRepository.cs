using Book.Domain.Aggregates.BookerAggregate;
using Book.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.Aggregates
{
    public interface IBookerRepository: IRepository<BookerAggregate.Booker>
    {
        Booker Add(Booker booker);
        Task<Booker> FindByNameAsync(string name);
        Task<IEnumerable<Booker>> FindAllByNameAsync(string name);
    }
}
