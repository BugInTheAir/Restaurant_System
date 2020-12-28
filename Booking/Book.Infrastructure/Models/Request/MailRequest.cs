using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Infrastructure.Models.Request
{
    public class MailRequest
    {
        public string Email { get; private set; }
        public string ContentSend { get; private set; }

        public MailRequest(string email, string contentSend)
        {
            Email = email;
            ContentSend = contentSend;
        }
    }
}
