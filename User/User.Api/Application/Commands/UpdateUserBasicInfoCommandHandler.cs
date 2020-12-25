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
    public class UpdateUserBasicInfoCommandHandler : IRequestHandler<UpdateUserBasicInfoCommand, bool>
    {
        private readonly UserManager<UserAccount> _userManager;
        public UpdateUserBasicInfoCommandHandler(UserManager<UserAccount> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
        public async Task<bool> Handle(UpdateUserBasicInfoCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _userManager.FindByNameAsync(request.UserName);
            if(userToUpdate == null)
            {
                throw new KeyNotFoundException();
            }
            else
            {
                var dto = request.Dto;
                userToUpdate.UpdateUser(dto.Phone, dto.Ward, dto.District, dto.Name, dto.Street);
                var result = await _userManager.UpdateAsync(userToUpdate);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Error when update user: " + result.Errors);
                }
            }
        }
    }
}
