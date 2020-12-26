using Restaurant.Domain.Aggregates.FoodAggregate;
using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Aggregates.RestaurantAggregate
{
    public interface IRestaurantRepository : IRepository<Restaurants>
    {
        Restaurants Add(Restaurants res);
        Restaurants Update(Restaurants res);
        Task<Restaurants> FindByIdAsync(string id);
        Task<Restaurants> FindByNameAsync(string name);
    }
}
