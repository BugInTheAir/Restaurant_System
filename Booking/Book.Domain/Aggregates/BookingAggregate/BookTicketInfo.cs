using Book.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Domain.Aggregates.BookingAggregate
{
    public class BookTicketInfo: ValueObject
    {
        public string AtDate { get; private set; }
        public int AtHour { get; private set; }
        public int AtMinute { get; private set; }
        public string Note { get; private set; }
        public BookTicketInfo(string atDate, int atHour, int atMinute, string note)
        {
            AtDate = atDate;
            AtHour = atHour;
            AtMinute = atMinute;
            Note = note;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AtDate;
            yield return AtHour;
            yield return AtMinute;
            yield return Note;
        }
    }
}
