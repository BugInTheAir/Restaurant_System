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
        Task RequestSendEmailVerification(EmailRequest content);
    }
    public class ExternalService : IExternalService
    {
        private static string EmailServer = "http://localhost:5005";
        public Task RequestSendEmailVerification(EmailRequest content)
        {
            var client = new RestClient(EmailServer);
            var request = new RestRequest("api/email/", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(content);
            return client.ExecuteAsync(request);
        }
    }
}
