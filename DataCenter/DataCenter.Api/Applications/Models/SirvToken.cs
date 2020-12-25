using DataCenter.Api.Applications.Configurations;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCenter.Api.Applications.Models
{
    public class SirvToken : IApiToken
    {
        public long ExpireDate { get; set; }
        public string Token { get; set; }

        public async Task<IApiToken> DoGetToken()
        {
            var client = new RestClient("https://api.sirv.com/v2/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"clientId\":\"" + Config.SIRV_CLIENT_ID + "\",\"clientSecret\":\"" + Config.SIRV_CLIENT_SECRET + "\"}", ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);
            IApiToken token = new SirvToken();
            var temp = JsonConvert.DeserializeObject<RecvToken>(response.Content);
            token.ExpireDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + long.Parse(temp.expiresIn);
            token.Token = temp.token;
            return token;
        }

        public bool IsExpire()
        {
            if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > ExpireDate)
            {
                return true;
            }
            return false;
        }
        public string TokenName()
        {
            return "SirvToken";
        }
    }
}
