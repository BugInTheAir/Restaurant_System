using Restaurant.Domain.Aggregates.Common;
using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Aggregates.FoodAggregate
{
    public class FoodItem : Entity, IAggregateRoot
    {
        public string TenantId { get; private set; }
        public FoodInfo FoodInfo { get; private set; }
        protected FoodItem()
        {
            TenantId = "FI" + "-" + DateTime.Now.Date.ToShortDateString() + "-" + Guid.NewGuid().ToString().Split("-")[0];
            _foodAndMenus = new List<FoodAndMenu>();
        }
        private readonly List<FoodAndMenu> _foodAndMenus;
        public IEnumerable<FoodAndMenu> FoodAndMenus => _foodAndMenus;

        public FoodItem(string imageUrl, string foodName, string description):this()
        {
            FoodInfo = new FoodInfo(imageUrl, foodName, description);
        }
        public void UpdateFoodInfo(string imageUrl, string foodName, string des)
        {
            this.FoodInfo = new FoodInfo(imageUrl ?? FoodInfo.ImageUrl,foodName ?? FoodInfo.FoodName, des ?? FoodInfo.Description);
        }
       
       
    }
}
