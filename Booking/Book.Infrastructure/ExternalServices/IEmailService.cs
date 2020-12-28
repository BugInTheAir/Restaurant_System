using Book.Infrastructure.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Book.Infrastructure.ExternalServices
{
    public interface IEmailService
    {
        void SendEmailAsync(MailRequest request);
    }
}
