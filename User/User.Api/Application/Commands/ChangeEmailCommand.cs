using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Application.Commands
{
    public class ChangeEmailCommand : IRequest<bool>
    {
        public string NewEmail { get; private set; }
        public string OldEmail { get; private set; }
        public string UserName { get; private set; }

        public ChangeEmailCommand(string newEmail, string oldEmail, string userName)
        {
            NewEmail = newEmail;
            OldEmail = oldEmail;
            UserName = userName;
        }
    }
}
