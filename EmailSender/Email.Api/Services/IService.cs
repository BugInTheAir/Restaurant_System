using Email.Api.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Email.Api.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailPayloadDTO dto);
    }
}
