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
    public class ResTypeRepository : IResTypeRepository
    {
        private readonly RestaurantContext _context;
        public IUnitOfWork UnitOfWork {
            get
            {
                return _context;
            }
        }

        public ResTypeRepository(RestaurantContext context)
        {
            _context = context;
        }

        public RestaurantType Add(RestaurantType res)
        {
            if (res.IsTransient())
            {
                return _context.RestaurantTypes.Add(res).Entity;
            }
            return res;
        }

        public async Task<RestaurantType> FindByIdAsync(string id)
        {
            var resType = await _context.RestaurantTypes.Where(x => x.TenantId.Equals(id)).FirstOrDefaultAsync();
            return resType;
        }

        public async Task<RestaurantType> FindByNameAsync(string name)
        {
            var resType = await _context.RestaurantTypes.Where(x => x.TypeName.Equals(name)).SingleOrDefaultAsync();
            return resType;
        }
    }
}
