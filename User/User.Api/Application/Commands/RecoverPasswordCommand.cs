using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Application.Commands
{
    public class RecoverPasswordCommand : IRequest<bool>
    {
        public string Email { get; private set; }
        public RecoverPasswordCommand(string email)
        {
            Email = email;
        }
    }
}
