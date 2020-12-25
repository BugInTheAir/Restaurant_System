using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Infrastructure.Models.Request;

namespace User.Infrastructure.ExternalIntegrationService
{
    public interface IExternalService
    {
        Task<bool> RequestSendEmailVerification(EmailRequest content);
    }
    public class ExternalService : IExternalService
    {
        private static string EmailServer = "http://localhost:5005";
        public async Task<bool> RequestSendEmailVerification(EmailRequest content)
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
