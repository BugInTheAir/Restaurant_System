using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.ViewModels
{
    public class RestaurantAddressChangeViewModel
    {
        public string ResId { get; set; }
        public string Street { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
    }
}
