using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using User.Api.Domain.Utils;
using User.Domains.Aggregate.UserAggregate;

namespace User.Api.Application.Commands
{
    public class ChangePasswordWithRecoveryTokenCommandHandler : IRequestHandler<ChangePasswordWithRecoveryTokenCommand, bool>
    {
        private readonly UserManager<UserAccount> _userManager;
        public ChangePasswordWithRecoveryTokenCommandHandler(UserManager<UserAccount> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> Handle(ChangePasswordWithRecoveryTokenCommand request, CancellationToken cancellationToken)
        {
            var findResult = await _userManager.FindByEmailAsync(request.Email);
            if (findResult == null)
                throw new KeyNotFoundException();
            request.Token = StringExtensions.Base64ForUrlDecode(request.Token);
            var result = await _userManager.ResetPasswordAsync(findResult, request.Token, request.NewPassword);
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(findResult);
                return true;
            }
            throw new Exception(result.Errors.ToString());
        }
    }
}
