using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCenter.Api.Applications.Models
{
    public class RecvToken
    {
        public string token { get; set; }
        public string expiresIn { get; set; }
    }
}
