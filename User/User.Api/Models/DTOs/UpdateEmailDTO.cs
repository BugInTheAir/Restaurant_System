using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Models.DTOs
{
    public class UpdateEmailDTO
    {
        public string EmailToUpdate { get; set; }
        public string OldEmail { get;  set; }

    }
}
