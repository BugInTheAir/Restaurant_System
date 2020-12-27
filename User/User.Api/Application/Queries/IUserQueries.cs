using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Models.DTOs;

namespace User.Api.Application.Queries
{
    public interface IUserQueries
    {
        Task<UserProfileDTO> GetUserProfile(string userName);
        Task<List<string>> GetAllEmails();
    }
}
