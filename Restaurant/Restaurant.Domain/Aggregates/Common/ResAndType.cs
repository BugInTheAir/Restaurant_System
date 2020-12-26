using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Aggregates.Common
{
    public class ResAndType : Entity, IAggregateRoot
    {
        public string TenantId { get; private set; }
        public string ResId { get; private set; }
        public string ResTypeId { get; private set; }
        protected ResAndType()
        {
            TenantId = $"RT-{DateTime.Now.ToShortDateString()}-{Guid.NewGuid().ToString().Split('-')[0]}";
        }
        public ResAndType(string resId, string resTypeId):this()
        {
            ResId = resId;
            ResTypeId = resTypeId;
        }
    }
}
