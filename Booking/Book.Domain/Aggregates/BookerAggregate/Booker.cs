using Book.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Domain.Aggregates.BookerAggregate
{
    public class Booker : Entity, IAggregateRoot
    {
        public string TenantId { get; private set; }
        public BookerInfo BookerInf { get; private set; } 
        public bool IsAnnonymous { get; private set; }
        protected Booker()
        {
            TenantId = $"BU-{DateTime.Now.ToShortDateString()}-{Guid.NewGuid().ToString().Split('-')[0]}";
        }
        public Booker(string userName, string email, string phone, string note) : this()
        {
            BookerInf = new BookerInfo(userName, email, phone);
        }
        public Booker GetAnnonymousBooker()
        {
            this.IsAnnonymous = true;
            return this;
        }
        public Booker GetUserBooker()
        {
            this.IsAnnonymous = false;
            return this;
        }
    }
}
