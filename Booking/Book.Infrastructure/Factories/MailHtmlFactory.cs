using System;
using System.Collections.Generic;
using System.Text;

namespace Book.Infrastructure.Models.Common
{
    public interface IMailHtmlFactory {
        string GenerateBookingTicketCreatedEmail(string bookId, string date, string restaurantName, string address);
    }
    public class MailHtmlFactory : IMailHtmlFactory
    {
        public string GenerateBookingTicketCreatedEmail(string bookId, string date, string restaurantName, string address)
        {
            return $"Thank you, your booking with id: {bookId} at {restaurantName} with address: {address}, on date: {date} has been created";
        }
    }
}
