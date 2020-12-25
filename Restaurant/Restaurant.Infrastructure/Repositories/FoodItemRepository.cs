using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Aggregates.FoodAggregate;
using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Repositories
{
    public class FoodItemRepository : IFoodRepository
    {
        private readonly RestaurantContext _context;
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();
        public FoodItemRepository(RestaurantContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(_context));
        }
        public FoodItem Add(FoodItem food)
        {
            if(food.IsTransient())
            {
                return _context.FoodItems.Add(food).Entity;
            }
            return food;
        }

        public async Task<FoodItem> FindByIdAsync(string id)
        {
            var food = await _context.FoodItems.Where(x => x.TenantId == id).FirstOrDefaultAsync();
            return food;
        }

        public FoodItem Update(FoodItem foodItem)
        {
            return _context.FoodItems
                  .Update(foodItem)
                  .Entity;
        }

        public async Task<FoodItem> FindByNameAsync(string name)
        {
            var foodItem = await _context.FoodItems.Where(x => x.FoodInfo.Equals(name)).FirstOrDefaultAsync();
            return foodItem;
        }
    }
}
