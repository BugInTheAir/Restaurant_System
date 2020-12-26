using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Aggregates.Common
{
    public class FoodAndMenu : Entity
    {
        public string TenantId { get; private set; }
        public string FoodId { get; private set; }
        public string MenuId { get; private set; }
        
        protected FoodAndMenu()
        {
            TenantId = $"FM-{DateTime.Now.ToShortDateString()}-{Guid.NewGuid().ToString().Split('-')[0]}";
        }
        public FoodAndMenu(string foodId, string menuId):this()
        {
            FoodId = foodId;
            MenuId = menuId;
        }

    }
}
