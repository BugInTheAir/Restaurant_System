using Email.Api.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Email.Api.Services
{
    public class EmailService : IEmailService
    {
        private readonly string senderEmail = "mailxacnhan99@gmail.com";
        private readonly string senderEmailPassword = "Xacnhan99";
        public void SendEmail(EmailPayloadDTO dto)
        {
            var smpt = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderEmailPassword)
            };
            using (var message = new MailMessage(senderEmail, dto.Email)
            {
                Subject = "Email verification to restaurant system",
                Body = dto.ContentSend,
                IsBodyHtml = true
            })
                smpt.Send(message);
        }
    }
}
