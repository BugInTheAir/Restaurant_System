using Restaurant.Domain.Aggregates.Common;
using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Aggregates.RestaurantAggregate
{
    public class RestaurantType : Entity, IAggregateRoot
    {
        public string TypeName { get; private set; }
        public string TenantId { get; private set; }
        public List<ResAndType> ResAndTypes { get; private set; }
        protected RestaurantType()
        {
            this.TenantId = "RT" + "-" + DateTime.Now.ToShortDateString() + "-" + Guid.NewGuid().ToString().Split("-")[0];
        }
        public RestaurantType(string typeName):this()
        {
            TypeName = typeName;
        }
    }
}
