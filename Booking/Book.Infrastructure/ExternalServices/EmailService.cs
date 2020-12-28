using Book.Infrastructure.Models.Request;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Book.Infrastructure.ExternalServices
{
    public class EmailService : IEmailService
    {
        private static string EmailServer = "http://localhost:5005";
        public void SendEmailAsync(MailRequest content)
        {
            var client = new RestClient(EmailServer);
            var request = new RestRequest("api/email/", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(content);
            client.ExecuteAsync(request);
          
        }
    }
}
