using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Aggregates.MenuAggregate
{
    public class MenuInfo : ValueObject
    {
        public string Name { get; private set; }
        public string Des { get; private set; }
        public MenuInfo(string name, string des)
        {
            this.Name = name;
            this.Des = des;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Des;
        }
    }
}
