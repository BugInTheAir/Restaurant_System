using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.ViewModels
{
    public class RestaurantWorkTimeChangeViewModel
    {
        public int OpenHour { get; set; }
        public int OpenMinute { get; set; }
        public int CloseHour { get; set; }
        public int CloseMinute { get; set; }
        public string ResId { get; set; }
    }
}
