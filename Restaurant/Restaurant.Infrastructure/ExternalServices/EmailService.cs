using Restaurant.Infrastructure.Models.Request;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.ExternalServices
{
    public class EmailService : IEmailService
    {
        private static string EmailServer = "http://localhost:5005";
        public async Task<bool> SendMail(EmailRequest content)
        {
            var client = new RestClient(EmailServer);
            var request = new RestRequest("api/email/", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(content);
            if ((await client.ExecuteAsync(request)).StatusCode.Equals(200))
            {
                return true;
            }
            return false;
        }
    }
}
