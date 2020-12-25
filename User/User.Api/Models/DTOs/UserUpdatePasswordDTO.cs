using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Models.DTOs
{
    public class UserUpdatePasswordDTO
    {  
        public string NewPassword { get; set; }
     
        public string ConfirmPassword { get;set; }
    }
}
