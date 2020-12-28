using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.ExternalServices
{
    public class UserService : IUserService
    {
        private string UserApiAddress = "http://localhost:5000/api/user";
        public async Task<List<string>> GetEmails()
        {
            var restClient = new RestClient($"{UserApiAddress}/email/all");
            var request = new RestRequest(Method.GET);
            var result = await restClient.ExecuteAsync<List<string>>(request);
            if(result.StatusCode == HttpStatusCode.OK)
            {
                return result.Data;
            }
            return null;

        }
    }
}
