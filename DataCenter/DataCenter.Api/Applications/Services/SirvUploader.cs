using DataCenter.Api.Applications.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCenter.Api.Applications.Services
{
    public class SirvUploader : IUploadImageService
    {
        public async Task<bool> UploadImageToServer(byte[] img, string fileName)
        {
            var client = new RestClient($"https://api.sirv.com/v2/files/upload?filename=%2Frestaurants%2F{fileName}");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "image/jpeg");
            request.AddHeader("authorization", $"Bearer {(await ApiConnection.getToken(new SirvToken())).Token}");
            request.AddParameter("application/json", img, ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;
            return false;
        }
    }
}
