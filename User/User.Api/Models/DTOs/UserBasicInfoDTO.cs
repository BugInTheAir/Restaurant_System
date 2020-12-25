using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Models.DTOs
{
    public class UserBasicInfoDTO
    {
        public string Ward { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
