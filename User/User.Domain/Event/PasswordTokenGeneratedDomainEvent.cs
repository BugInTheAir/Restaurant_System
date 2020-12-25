using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.Domain.Event
{
    public class PasswordTokenGeneratedDomainEvent : INotification
    {
        public string Token { get; private set; }
        public string EmailToSend { get; private set; }
        public PasswordTokenGeneratedDomainEvent(string token, string emailToSend)
        {
            Token = token;
            EmailToSend = emailToSend;
        }
    }
}
