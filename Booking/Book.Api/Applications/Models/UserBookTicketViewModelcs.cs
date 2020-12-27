using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Api.Applications.Models
{
    public class UserBookTicketViewModel
    {
        public string UserName { get; set; }
        public string RestaurantName { get;  set; }
        public string AtDate { get;  set; }
        public string Status { get;  set; }
        public string TicketId { get;  set; }
    }
}
