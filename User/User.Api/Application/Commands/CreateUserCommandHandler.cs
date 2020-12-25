using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.Aggregate.UserAggregate;
using User.Domains.Aggregate.UserAggregate;

namespace User.Api.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(UserManager<UserAccount> userManager, RoleManager<IdentityRole> roleManager, IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentException(nameof(_userRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(_userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(_roleManager));
        }
        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.ToApplicationUser();
            try
            {
                if(await user.RegisterNewUser(request.Email,request.Password, _userManager, _roleManager))
                {
                    await _userRepository.UnitOfWork.SaveEntitiesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
