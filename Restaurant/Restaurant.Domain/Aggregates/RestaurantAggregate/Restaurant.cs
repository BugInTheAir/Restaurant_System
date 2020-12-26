using Restaurant.Domain.Aggregates.Common;
using Restaurant.Domain.Events;
using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Aggregates.RestaurantAggregate
{
    public class Restaurants : Entity, IAggregateRoot
    {
        public string TenantId { get; private set; }
        public Address Address { get; private set; }
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public int Seats { get; private set; }
        public WorkTime WorkTime { get; private set; }
        private readonly List<ResImage> _resImages;
        private readonly List<ResAndType> _resAndTypes;
        public IEnumerable<ResAndType> ResAndTypes => _resAndTypes; 
        public IEnumerable<ResImage> ResImages => _resImages;
        private readonly List<RestaurantAndMenu> _restaurantAndMenus;
        public IEnumerable<RestaurantAndMenu> RestaurantAndMenus => _restaurantAndMenus;
        protected Restaurants()
        {
            _resImages = new List<ResImage>();
            _resAndTypes = new List<ResAndType>();
            _restaurantAndMenus = new List<RestaurantAndMenu>();
            this.TenantId = "Res-" + DateTime.Now.ToShortDateString() + "-" + Guid.NewGuid().ToString().Split("-")[0];
        }
        public Restaurants(string name, string street, string district, string ward, string openTime, string closeTime,string phone ,int seats, List<string> resAndType,List<string> menus, List<ResImage> images):this()
        {
            if (seats <= 0)
                throw new Exception("Invalid seats");
            Name = name;
            Seats = seats;
            Address = new Address(street, district, ward);
            WorkTime = new WorkTime(openTime, closeTime);
            Phone = phone;
            var verifiedItems = menus == null || menus.Count == 0 ? throw new Exception("Invalid menus") : menus;
            foreach(var item in verifiedItems)
            {
                _restaurantAndMenus.Add(new RestaurantAndMenu(this.TenantId, item));
            }
            _resImages = images;
            foreach(var item in resAndType)
            {
                _resAndTypes.Add(new ResAndType(this.TenantId, item));
            }
            List<string> url = new List<string>();
            images.ForEach(x =>
            {
                url.Add(x.ImageUrl);
            });
            AddDomainEvent(new NewRestaurantCreatedDomainEvent(TenantId, Name, Phone,Address, WorkTime, verifiedItems, url));
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
