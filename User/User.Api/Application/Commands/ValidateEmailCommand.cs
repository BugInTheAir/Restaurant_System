using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Application.Commands
{
    public class ValidateEmailCommand : IRequest<bool>
    {
        public string EmailToValidate { get; private set; }
        public ValidateEmailCommand(string email)
        {
            EmailToValidate = email;
        }
    }
}
