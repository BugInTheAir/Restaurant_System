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
    public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, bool>
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly IUserRepository _userRepository;

        public ChangeEmailCommandHandler(UserManager<UserAccount> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            var searchResult = await _userManager.FindByNameAsync(request.UserName);
            if (searchResult == null)
                throw new KeyNotFoundException();
            try
            {
                await searchResult.ChangeEmail(request.OldEmail, request.NewEmail, _userManager);
                _userRepository.Update(searchResult);
                return await _userRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
