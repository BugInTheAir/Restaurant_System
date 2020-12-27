using Book.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Domain.Aggregates.BookingAggregate
{
    public class BookTicketInfo: ValueObject
    {
        public string BookerId { get; private set; }
        public string AtDate { get; private set; }
        public string AtHour { get; private set; }
        public string AtMinute { get; private set; }
        public string Note { get; private set; }
        public BookTicketInfo(string bookerId, string atDate, string atHour, string atMinute, string note)
        {
            BookerId = bookerId;
            AtDate = atDate;
            AtHour = atHour;
            AtMinute = atMinute;
            Note = note;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return BookerId;
            yield return AtDate;
            yield return AtHour;
            yield return AtMinute;
            yield return Note;
        }
    }
}
