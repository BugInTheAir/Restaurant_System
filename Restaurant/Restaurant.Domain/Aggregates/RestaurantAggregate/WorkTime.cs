using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Aggregates.RestaurantAggregate
{
    public class WorkTime : ValueObject
    {
        public string OpenTime { get; private set; }
        public string CloseTime { get; private set; }

        public WorkTime(string openTime, string closeTime)
        {
            OpenTime = openTime;
            CloseTime = closeTime;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return OpenTime;
            yield return CloseTime;
        }
    }
}
