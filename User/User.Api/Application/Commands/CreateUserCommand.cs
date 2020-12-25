using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using User.Api.Models.DTOs;
using User.Domains.Aggregate.UserAggregate;

namespace User.Api.Application.Commands
{
    [DataContract]
    public class CreateUserCommand : IRequest<bool>
    {

        [DataMember]

        public string Email { get; private set; }
        [DataMember]

        public string Password { get; private set; }

        public CreateUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public UserAccount ToApplicationUser()
        {
            return new UserAccount("","",this.Email,this.Email,"");
        }
    }
}
