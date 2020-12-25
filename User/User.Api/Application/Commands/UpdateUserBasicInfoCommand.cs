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
    public class UpdateUserBasicInfoCommand : IRequest<bool>
    {
        [DataMember]
        public String UserName { get; private set; }
        [DataMember]
        public UserBasicInfoDTO Dto { get; private set; }
        public UpdateUserBasicInfoCommand(UserBasicInfoDTO dto, string userName)
        {
            Dto = dto;
            UserName = userName;
        }
    }
}
