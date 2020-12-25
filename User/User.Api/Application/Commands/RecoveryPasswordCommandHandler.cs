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
    public class RecoveryPasswordCommandHandler : IRequestHandler<RecoverPasswordCommand, bool>
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly IUserRepository _userRepository;
        public RecoveryPasswordCommandHandler(UserManager<UserAccount> userManager, IUserRepository userRepository)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(_userManager));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(_userRepository));
        }
        public async Task<bool> Handle(RecoverPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new KeyNotFoundException();
            _userRepository.Update(await user.RecoveryPassword(_userManager,request.Email));
            return await _userRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
