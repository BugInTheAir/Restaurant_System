using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Application.Commands
{
    public class ChangePasswordWithRecoveryTokenCommand : IRequest<bool>
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public ChangePasswordWithRecoveryTokenCommand(string token, string email, string newPass)
        {
            Token = token;
            Email = email;
            NewPassword = newPass;
        }
    }
}
