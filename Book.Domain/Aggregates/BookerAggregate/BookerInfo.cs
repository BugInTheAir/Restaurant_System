using Book.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Domain.Aggregates.BookerAggregate
{
    public class BookerInfo : ValueObject
    {
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public BookerInfo(string userName, string email, string phone)
        {
            UserName = userName;
            Email = email;
            Phone = phone;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserName;
            yield return Email;
            yield return Phone;
        }
    }
}
