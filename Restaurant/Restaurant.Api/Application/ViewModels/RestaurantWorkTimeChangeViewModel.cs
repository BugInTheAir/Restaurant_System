using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.ViewModels
{
    public class RestaurantWorkTimeChangeViewModel
    {
        public string OpenHour { get; set; }
        public string OpenMinute { get; set; }
        public string CloseHour { get; set; }
        public string CloseMinute { get; set; }
        public string ResId { get; set; }
    }
}
