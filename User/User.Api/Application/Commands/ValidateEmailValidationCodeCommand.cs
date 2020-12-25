using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Application.Commands
{
    public class ValidateEmailValidationCodeCommand : IRequest<bool>
    {
        public string Code { get; private set; }
        public string Email { get; private set; }
        public ValidateEmailValidationCodeCommand(string code, string email)
        {
            Code = code;
            Email = email;
        }
    }
}
