using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Infrastructure.ExternalServices
{
    public interface IUserService
    {
        List<string> GetEmails();
    }
}
