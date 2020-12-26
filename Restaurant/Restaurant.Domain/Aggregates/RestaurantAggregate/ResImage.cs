using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Aggregates.RestaurantAggregate
{
    public class ResImage : Entity
    {
        public string ImageUrl { get; private set; }
        public string FileExt { get; private set; }
        protected ResImage() { }
        public ResImage(string url,string ext)
        {
            ImageUrl = url;
            FileExt = ext;
        }
     
    }
}
