using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Aggregates.RestaurantAggregate
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string District { get; private set; }
        public string Ward { get; private set; }
        public Address() { }

        public Address (string street, string district, string ward)
        {
            Street = street;
            District = district;
            Ward = ward;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return District;
            yield return Ward;
        }
    }
}
