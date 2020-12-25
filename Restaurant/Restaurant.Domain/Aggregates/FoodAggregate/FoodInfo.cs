using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Aggregates.FoodAggregate
{
    public class FoodInfo : ValueObject
    {
        public FoodInfo(string imageUrl, string foodName, string description)
        {
            ImageUrl = imageUrl;
            FoodName = foodName;
            Description = description;
        }

        public string ImageUrl { get; private set; }
        public string FoodName { get; private set; }
        public string Description { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FoodName;
            yield return Description;
            yield return ImageUrl;
        }
    }
}
