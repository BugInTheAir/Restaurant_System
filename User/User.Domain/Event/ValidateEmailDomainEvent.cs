using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.Domain.Event
{
    public class ValidateEmailDomainEvent : INotification
    {
        public string EmailToValidate { get; private set; }
        public string ContentToSend { get; private set; }
        public string ValidationCode { get; private set; }
        public ValidateEmailDomainEvent(string email, string content,string code)
        {
            EmailToValidate = email;
            ContentToSend = content;
            ValidationCode = code;
        }
    }
}
