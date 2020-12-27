using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Api.Applications.Models
{
    public class UserBookingViewModel
    {
        public string UserName { get; set; }
        public string AtDate { get; set; }
        public int AtHour { get; set; }
        public int AtMinute { get; set; }
        public string Note { get; set; }
        public string ResId { get; set; }
    }
}
