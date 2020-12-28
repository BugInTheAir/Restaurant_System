using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.ExternalServices
{
    public interface IUserService
    {
        Task<List<string>> GetEmails();
    }
}
