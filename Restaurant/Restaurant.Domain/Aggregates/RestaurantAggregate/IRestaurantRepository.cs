using Restaurant.Domain.Aggregates.FoodAggregate;
using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Aggregates.RestaurantAggregate
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        Restaurant Add(Restaurant res);
        Restaurant Update(Restaurant res);
        Task<Restaurant> FindByIdAsync(string id);
        Task<Restaurant> FindByNameAsync(string name);
    }
}
