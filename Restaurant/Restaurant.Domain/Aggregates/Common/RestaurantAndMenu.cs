using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Aggregates.Common
{
    public class RestaurantAndMenu : Entity
    {
        public string TenantId { get; private set; }
        public string ResId { get; private set; }
        public string MenuId { get; private set; }
        protected RestaurantAndMenu()
        {
            TenantId = "RM-" + DateTime.Now.ToShortDateString() + "-" + Guid.NewGuid().ToString().Split(" ")[0];
        }
        public RestaurantAndMenu(string resId, string menuId):this()
        {
            ResId = resId;
            MenuId = menuId;
        }
    }
}
