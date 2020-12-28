using Book.Infrastructure.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Book.Infrastructure.ExternalServices
{
    public interface IResService
    {
        Task<RestaurantInfoResponse> GetRestaurantInfo(string id);
    }
}
