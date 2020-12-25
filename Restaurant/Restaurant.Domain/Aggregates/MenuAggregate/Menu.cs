using Restaurant.Domain.Aggregates.Common;
using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.Domain.Aggregates.MenuAggregate
{
    public class Menu : Entity, IAggregateRoot
    {
        public string TenantId { get; private set; }
        public MenuInfo MenuInfo { get; private set; }


        private readonly List<FoodAndMenu> _foodAndMenus;
        public IEnumerable<FoodAndMenu> FoodAndMenus => _foodAndMenus.AsReadOnly();
        public List<RestaurantAndMenu> RestaurantAndMenus { get; private set; }

        protected Menu()
        {
            TenantId = $"M-{DateTime.Now.ToShortDateString()}-{Guid.NewGuid().ToString().Split('-')[0]}"; 
            _foodAndMenus = new List<FoodAndMenu>();
        }

        public Menu(string name, string description, List<string> foodId): this()
        {
            MenuInfo = new MenuInfo(name, description);
            foreach(var id in foodId)
            {
                _foodAndMenus.Add(new FoodAndMenu(id, this.TenantId));
            }
        }

        public void RemoveFoodFromMenu(string foodId)
        {
            if (_foodAndMenus.Count == 1)
                throw new Exception("Menu must have at least 1 food item");
            var existingFoodMenu = _foodAndMenus.Where(x => x.FoodId == foodId && x.MenuId == this.TenantId).SingleOrDefault();
            if(existingFoodMenu != null)
            {
                _foodAndMenus.Remove(existingFoodMenu);
            }
        }
        public void AddFoodToMenu(string foodId)
        {
            var existingFoodMenu = _foodAndMenus.Where(x => x.FoodId == foodId && x.MenuId == this.TenantId).SingleOrDefault();
            if(existingFoodMenu == null)
            {
                _foodAndMenus.Add(new FoodAndMenu(foodId, this.TenantId));
            }
        }

        public void ChangeMenuDescription(string des)
        {
            this.MenuInfo = new MenuInfo(this.MenuInfo.Name, des);
        }

    }
}
