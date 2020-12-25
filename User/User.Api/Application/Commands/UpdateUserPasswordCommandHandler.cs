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
    public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, bool>
    {
        private readonly UserManager<UserAccount> _userManager;
        public UpdateUserPasswordCommandHandler(UserManager<UserAccount> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var dto = request.DTO;
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                throw new KeyNotFoundException();
            var result = await _userManager.ChangePasswordAsync(user, dto.ConfirmPassword, dto.NewPassword);
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
