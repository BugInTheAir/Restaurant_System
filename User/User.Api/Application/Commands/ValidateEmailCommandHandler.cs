using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using User.Domains.Aggregate.UserAggregate;

namespace User.Api.Application.Commands
{
    public class ValidateEmailCommandHandler : IRequestHandler<ValidateEmailCommand, bool>
    {
        private readonly UserManager<UserAccount> _userManager;
        public ValidateEmailCommandHandler(UserManager<UserAccount> userManager)
        {
            _userManager = userManager;
        }
        public Task<bool> Handle(ValidateEmailCommand request, CancellationToken cancellationToken)
        {
            UserAccount userAccount = new UserAccount();
            return userAccount.CreateEmailValidationCode(request.EmailToValidate,"Your email verification link is: ",_userManager);
        }
    }
}
