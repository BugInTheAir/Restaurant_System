using Book.Infrastructure.Models.Response;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Book.Infrastructure.ExternalServices
{
    public class ResService : IResService
    {
        private string _resAddress = "http://localhost:5001/api/restaurant";
        public async Task<RestaurantInfoResponse> GetRestaurantInfo(string id)
        {
            var restClient = new RestClient(_resAddress);
            var request = new RestRequest(Method.GET);
            request.AddParameter("id", id);
            var result = await restClient.ExecuteAsync<RestaurantInfoResponse>(request);
            if(result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return result.Data;
            }
            else
            {
                return null;
            }
        }
    }
}
