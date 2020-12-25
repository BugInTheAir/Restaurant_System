using Restaurant.Domain.Aggregates.Common;
using Restaurant.Domain.Events;
using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Aggregates.RestaurantAggregate
{
    public class Restaurant : Entity, IAggregateRoot
    {
        public string TenantId { get; private set; }
        public Address Address { get; private set; }
        public string Name { get; private set; }
        public int Seats { get; private set; }
        public RestaurantType RestaurantType { get; private set; }
        public WorkTime WorkTime { get; private set; }
        private readonly List<RestaurantAndMenu> _restaurantAndMenus;
        public IEnumerable<RestaurantAndMenu> RestaurantAndMenus => _restaurantAndMenus;
        protected Restaurant()
        {
            _restaurantAndMenus = new List<RestaurantAndMenu>();
            this.TenantId = "Res-" + DateTime.Now.ToShortDateString() + "-" + Guid.NewGuid().ToString().Split("-")[0];
        }
        public Restaurant(string name, string street, string district, string ward,string openTime, string closeTime,int seats, List<string> menus):this()
        {
            if (seats <= 0)
                throw new Exception("Invalid seats");
            Name = name;
            Seats = seats;
            Address = new Address(street, district, ward);
            WorkTime = new WorkTime(openTime, closeTime);
            var verifiedItems = menus == null || menus.Count == 0 ? throw new Exception("Invalid menus") : menus;
            foreach(var item in verifiedItems)
            {
                _restaurantAndMenus.Add(new RestaurantAndMenu(this.TenantId, item));
            }
            AddDomainEvent(new NewRestaurantCreatedDomainEvent(TenantId, Name, Address, WorkTime, verifiedItems));
        }
        //Bussiness
        public void AssignMenuToRestaurant(string menuId)
        {
            var verifiedMenu = menuId ?? throw new ArgumentNullException("Invalid menu id");
            _restaurantAndMenus.Add(new RestaurantAndMenu(this.TenantId, verifiedMenu));
            AddDomainEvent(new NewMenuAddedInRestaurant(Name, this.Address, menuId));
        }
        public void RemoveMenuFromRestaurant(string menuId)
        {
            var existedMenu = _restaurantAndMenus.Find(x => x.MenuId.Equals(menuId));
            if (existedMenu == null)
                throw new KeyNotFoundException();
            if (_restaurantAndMenus.Count == 1)
                throw new Exception("At least one menu in restaurant");
            _restaurantAndMenus.Remove(existedMenu);
            AddDomainEvent(new MenuRemovedFromRestaurant(Name, Address, menuId));
        }
        public void UpdateAddress(string street, string district, string ward)
        {
            var oldAddress = this.Address;
            this.Address = new Address(street, district, ward);
            AddDomainEvent(new RestaurantAddressChangedDomainEvent(this.Name, oldAddress, this.Address));
        }
        public void UpdateWorkTime(string openTime, string closeTime)
        {
            this.WorkTime = new WorkTime(openTime ?? WorkTime.OpenTime, closeTime ?? WorkTime.CloseTime);
            AddDomainEvent(new WorkHourChangedDomainEvent(Name, Address, WorkTime));
        }
        public void UpdateSeats(int seats)
        {
            if (seats <= 0)
                throw new Exception("Invalid seats");
            Seats = seats;
        }
    }
}
