using MediatR;
using Restaurant.Domain.Aggregates.RestaurantAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Events
{
    public class NewMenuAddedInRestaurantDomainEvent : INotification
    {
        public string RestaurantName { get; private set; }
        public Address Address { get; private set; }
        public string MenuItem { get; private set; }

        public NewMenuAddedInRestaurantDomainEvent(string restaurantName, Address address, string menuItem)
        {
            RestaurantName = restaurantName;
            Address = address;
            MenuItem = menuItem;
        }
    }
}
