using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Aggregates.FoodAggregate
{
    public interface IFoodRepository : IRepository<FoodItem>
    {
        FoodItem Add(FoodItem food);
        FoodItem Update(FoodItem foodItem);
        Task<FoodItem> FindByIdAsync(string id);
        Task<FoodItem> FindByNameAsync(string name);
    }
}
