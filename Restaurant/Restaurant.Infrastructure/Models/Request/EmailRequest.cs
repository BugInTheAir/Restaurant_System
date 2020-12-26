using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Infrastructure.Models.Request
{
    public class EmailRequest
    {
        public string Email { get; private set; }
        public string ContentSend { get; private set; }

        public EmailRequest(string email, string contentSend)
        {
            Email = email;
            ContentSend = contentSend;
        }
    }
}