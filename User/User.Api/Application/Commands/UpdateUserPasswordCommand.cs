using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using User.Api.Models.DTOs;

namespace User.Api.Application.Commands
{
    [DataContract]
    public class UpdateUserPasswordCommand : IRequest<bool>
    {
        [DataMember]
        public UserUpdatePasswordDTO DTO { get; private set; }
        [DataMember]
        public string UserName { get; private set; }
        public UpdateUserPasswordCommand(UserUpdatePasswordDTO dto, string userName)
        {
            DTO = dto;
            UserName = userName;
        }
    }
}
