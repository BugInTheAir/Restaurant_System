using MediatR;
using Restaurant.Domain.Aggregates.RestaurantAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Events
{
    public class RestaurantAddressChangedDomainEvent : INotification
    {
        public string RestaurantName { get; private set; }
        public Address OldAddress { get; private set; }
        public Address NewAddress { get; private set; }
        public string ToAddress(Address address)
        {
            return $"{address.Street},{address.Ward},{address.District}";
        }
        public RestaurantAddressChangedDomainEvent(string restaurantName, Address oldAddress, Address newAddress)
        {
            RestaurantName = restaurantName;
            OldAddress = oldAddress;
            NewAddress = newAddress;
        }
    }
}
