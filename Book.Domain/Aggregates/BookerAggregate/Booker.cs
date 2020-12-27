using Book.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Domain.Aggregates.BookerAggregate
{
    public class Booker : Entity, IAggregateRoot
    {
        public string TenantId { get; private set; }
        public BookerInfo BookInf { get; private set; }
        protected Booker()
        {
            TenantId = $"BU-{DateTime.Now.ToShortDateString()}-{Guid.NewGuid().ToString().Split('-')[0]}";
        }
        public Booker(string userName, string email, string phone, string note) : this()
        {
            BookInf = new BookerInfo(userName, email, phone);
        }

    }
}
