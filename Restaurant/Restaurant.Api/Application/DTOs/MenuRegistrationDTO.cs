using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.DTOs
{
    public class MenuRegistrationDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> FoodItems { get; set; }
    }
}
