using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Api.Applications.Models
{
    public class UserBookTicketViewModelcs
    {
        public string RestaurantName { get; private set; }
        public string AtDate { get; private set; }
        public string Status { get; private set; }
        public string TicketId { get; private set; }
    }
}
