using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Aggregates.MenuAggregate;
using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private RestaurantContext _context;
        public IUnitOfWork UnitOfWork {
            get
            {
                return _context;
            }
        }
        public MenuRepository(RestaurantContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Menu Add(Menu menu)
        {
            if (menu.IsTransient())
            {
                return _context.Menus.Add(menu).Entity;
            }
            else
            {
                return menu;
            }
        }

        public async Task<Menu> FindByIdAsync(string id)
        {
            var menu = await _context.Menus.Include(m => m.FoodAndMenus).Where(m => m.TenantId == id).FirstOrDefaultAsync();
            return menu;
        }
  
        public Menu Update(Menu menu)
        {
            return _context.Menus
                   .Update(menu)
                   .Entity;
        }

        public async Task<Menu> FindByNameAsync(string name)
        {
            var menu = await _context.Menus.Include(m => m.FoodAndMenus).Where(m => m.MenuInfo.Name == name).FirstOrDefaultAsync();
            return menu;
        }
    }
}
