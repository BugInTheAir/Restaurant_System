using MediatR;
using Restaurant.Domain.Aggregates.RestaurantAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Events
{
    public class NewRestaurantCreatedDomainEvent : INotification
    {
        public string RestaurantName { get; private set; }
        public Address Address { get; private set; }
        public WorkTime WorkTime { get; private set; }
        public string ResId { get; private set; }
        public string Phone { get; private set; }
        public List<string> MenuIds { get; private set; }
        public List<string> Images { get; private set; }
        public NewRestaurantCreatedDomainEvent(string resId,string restaurantName,string phone,Address address, WorkTime workTime, List<string> menus, List<string> images)
        {
            RestaurantName = restaurantName;
            Address = address;
            WorkTime = workTime;
            ResId = resId;
            MenuIds = menus;
            Phone = phone;
            Images = images;
        }
    }
}
