using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCenter.Api.Applications.Models
{
    public interface IApiToken
    {
        long ExpireDate { get; set; }
        string Token { get; set; }
        bool IsExpire();
        string TokenName();
        Task<IApiToken> DoGetToken();
    }
}
