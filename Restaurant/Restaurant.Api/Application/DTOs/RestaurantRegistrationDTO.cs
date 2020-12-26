using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.DTOs
{
    public class RestaurantRegistrationDTO
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string Phone { get; set; }
        public string TypeId { get; set; }
        public string Seats { get; set; }
        public string OpenTime{ get; set; }
        public string CloseTime{ get; set; }
        public List<string> Menus{ get; set; }
    }
}
