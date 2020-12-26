using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Aggregates.RestaurantAggregate;
using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private RestaurantContext _context;
        public IUnitOfWork UnitOfWork {
            get
            {
                return _context;
            }
        }
        public RestaurantRepository(RestaurantContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(_context));
        }
        public Restaurants Add(Restaurants res)
        {
            if (res.IsTransient())
            {
                return _context.Restaurants.Add(res).Entity;
            }
            else
            {
                return res;
            }
        }

        public async Task<Restaurants> FindByIdAsync(string id)
        {
            var res = await _context.Restaurants.Include(m => new { m.RestaurantAndMenus, m.RestaurantType }).Where(m => m.TenantId == id).FirstOrDefaultAsync();
            return res;
        }

        public Restaurants Update(Restaurants res)
        {
            return _context.Restaurants
                   .Update(res)
                   .Entity;
        }

        public async Task<Restaurants> FindByNameAsync(string name)
        {
            var res = await _context.Restaurants.Include(m => new { m.RestaurantAndMenus, m.RestaurantType }).Where(m => m.Name == name).FirstOrDefaultAsync();
            return res;
        }
    }
}
