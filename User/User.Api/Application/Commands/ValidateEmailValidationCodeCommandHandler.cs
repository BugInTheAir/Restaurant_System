using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using User.Api.Application.Utils;
using User.Api.Domain.Utils;
using User.Domains.Aggregate.UserAggregate;

namespace User.Api.Application.Commands
{
    public class ValidateEmailValidationCodeCommandHandler : IRequestHandler<ValidateEmailValidationCodeCommand, bool>
    {
        private readonly UserManager<UserAccount> _userManager;
        public ValidateEmailValidationCodeCommandHandler(UserManager<UserAccount> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(_userManager));
        }
        public async Task<bool> Handle(ValidateEmailValidationCodeCommand request, CancellationToken cancellationToken)
        {
            var searchResult =await _userManager.FindByEmailAsync(request.Email);
            if (searchResult == null)
                throw new KeyNotFoundException();
            var result = await _userManager.ConfirmEmailAsync(searchResult, StringExtensions.Base64ForUrlDecode(request.Code));
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                throw new Exception(result.Errors.ToString());
            }
        }
    }
}
