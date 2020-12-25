using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using User.Api.Domain.Utils;
using User.Domain.Event;
using User.Domain.SeedWorks;

namespace User.Domains.Aggregate.UserAggregate
{
    public class UserAccount : IdentityUser, IAggregateRoot
    {
        private List<INotification> _domainEvents;

        private string VerifyEmailUrl = "http://25.81.40.169:5000/api/user/email/validate?code=";

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public string Phone { get; private set; }
        public string Street { get; private set; }
        public string Ward { get; private set; }
        public string District { get; private set; }
        public string Name { get; private set; }
        public bool IsPasswordRecover { get; private set; }
        public UserAccount()
        {

        }

        public UserAccount(string district, string street, string email, string name, string ward)
        {
            District = district;
            Street = street;
            Email = email;
            Ward = ward;
            Name = name;
            UserName = email;
        }

        public static UserAccount GetDraft()
        {
            return new UserAccount();
        }
        public void UpdateUser(string phone, string ward, string district, string name, string street)
        {
            this.Phone = phone;
            this.Ward = ward;
            this.District = district;
            this.Name = name;
            this.Street = street;
        }
        public async Task<bool> RegisterNewUser(string email, string password, UserManager<UserAccount> userManager, RoleManager<IdentityRole> roleManager)
        {

            var searchResult = await userManager.FindByNameAsync(email);
            if (searchResult != null)
            {
                throw new Exception("Invalid user");
            }
            var result = await userManager.CreateAsync(this, password);
            searchResult = await userManager.FindByNameAsync(email);
            var token = await userManager.GenerateEmailConfirmationTokenAsync(searchResult);
            token = StringExtensions.Base64ForUrlEncode(token);
            AddDomainEvent(new ValidateEmailDomainEvent(email, "Welcome to our system, here is your email validation link: " + VerifyEmailUrl + token + $"&email={email}", token));
            string role = "User";

            if (result.Succeeded)
            {
                if (await userManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
                await userManager.AddToRoleAsync(this, role);
                await userManager.AddClaimAsync(this, new System.Security.Claims.Claim("userName", this.UserName));
                await userManager.AddClaimAsync(this, new System.Security.Claims.Claim("name", this.Name));
                await userManager.AddClaimAsync(this, new System.Security.Claims.Claim("email", this.Email));
                await userManager.AddClaimAsync(this, new System.Security.Claims.Claim("role", role));
                return true;
            }
            else
            {
                ClearDomainEvents();
                throw new Exception(result.Errors.ToString());
            }
        }
        public async Task<bool> CreateEmailValidationCode(string email,string description, UserManager<UserAccount> userManager)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                throw new KeyNotFoundException();
            AddDomainEvent(new ValidateEmailDomainEvent(email, description,await userManager.GenerateEmailConfirmationTokenAsync(user)));
            return true;
        }
        public async Task<UserAccount> RecoveryPassword(UserManager<UserAccount> userManager, string email)
        {
            await userManager.UpdateSecurityStampAsync(this);
            var token = await userManager.GeneratePasswordResetTokenAsync(this);
            token = StringExtensions.Base64ForUrlEncode(token);
            AddDomainEvent(new PasswordTokenGeneratedDomainEvent(token, email));
            this.IsPasswordRecover = false;
            return this;
        }
        public async Task<bool> ChangeEmail(string oldEmail, string newEmail, UserManager<UserAccount> userManager)
        {
            if (this.Email != oldEmail)
                throw new Exception("Invalid email");
            this.Email = newEmail;
            this.EmailConfirmed = false;
            var token = await userManager.GenerateEmailConfirmationTokenAsync(this);
            token = StringExtensions.Base64ForUrlEncode(token);
            AddDomainEvent(new ValidateEmailDomainEvent(newEmail, "Your email validation link: " + VerifyEmailUrl + token + $"&email={newEmail}", token));
            await userManager.RemoveClaimAsync(this, (await userManager.GetClaimsAsync(this)).Where(x => x.Type == "email").FirstOrDefault());
            await userManager.AddClaimAsync(this, new System.Security.Claims.Claim("email", this.Email));
            return true;
        }
        
        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

    }
}
