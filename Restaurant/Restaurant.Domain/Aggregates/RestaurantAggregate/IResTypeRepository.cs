using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Aggregates.RestaurantAggregate
{
    public interface IResTypeRepository : IRepository<RestaurantType>
    {
        RestaurantType Add(RestaurantType res);
        Task<RestaurantType> FindByNameAsync(string name);
        Task<RestaurantType> FindByIdAsync(string id);
    }
}
