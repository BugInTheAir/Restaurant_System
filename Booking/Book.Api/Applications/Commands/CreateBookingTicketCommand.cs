using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Api.Applications.Commands
{
    public class CreateBookingTicketCommand : IRequest<bool>
    {
        public string ResId { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Note { get; private set; }
        public string BookDate { get; private set; }
        public int AtHour { get; private set; }
        public int AtMinute { get; private set; }
        public bool IsAnnonymous { get; private set; }

        public CreateBookingTicketCommand(string resId,string userName, string email, string phone, string note, string bookDate, int atHour, int atMinute, bool isAnnonymous)
        {
            ResId = resId;
            UserName = userName;
            Email = email;
            Phone = phone;
            Note = note;
            BookDate = bookDate;
            AtHour = atHour;
            AtMinute = atMinute;
            IsAnnonymous = isAnnonymous;
        }
    }
}
