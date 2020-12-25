using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Domains.Aggregate.UserAggregate;
using User.Infrastructure;

namespace User.Api.Infrastructure.Configuration
{
    public class IdentityProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<UserAccount> _claimsFactory;
        private readonly UserManager<UserAccount> _userManager;

        public IdentityProfileService(IUserClaimsPrincipalFactory<UserAccount> claimsFactory, UserManager<UserAccount> userManager)
        {
            _claimsFactory = claimsFactory;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var role = await _userManager.GetRolesAsync(user);
            if (user == null)
            {
                throw new ArgumentException("");
            }
            var claims = new List<System.Security.Claims.Claim>();
            claims.Add(new System.Security.Claims.Claim("username",user.UserName));
            foreach(var item in role)
            {
                claims.Add(new System.Security.Claims.Claim("role", item));
            }
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
