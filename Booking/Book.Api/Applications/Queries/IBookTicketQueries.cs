using Book.Api.Applications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Api.Applications.Queries
{
    public interface IBookTicketQueries
    {
        Task<UserBookTicketViewModelcs> GetUserBookTickets(string userName);
    }
}
