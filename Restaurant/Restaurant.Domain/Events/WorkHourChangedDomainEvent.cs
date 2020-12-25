using MediatR;
using Restaurant.Domain.Aggregates.RestaurantAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Events
{
    public class WorkHourChangedDomainEvent : INotification
    {
        public string RestaurantName { get; private set; }
        public Address Address { get; private set; }
        public WorkTime ChangedTime { get; private set; }

        public WorkHourChangedDomainEvent(string restaurantName, Address address, WorkTime changedTime)
        {
            RestaurantName = restaurantName;
            Address = address;
            ChangedTime = changedTime;
        }
    }
}
