using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Models.DTOs;

namespace User.Api.Application.Queries
{
    public class UserQueries : IUserQueries
    {
        private string _connectionString = string.Empty;
        public UserQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<UserProfileDTO> GetUserProfile(string userName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>(@"select Name as name, UserName as username, Street as street, Ward as ward, District as district, Phone as phone, Email as email, EmailConfirmed as emailconfirmed from dbo.AspNetUsers where UserName = @userName", new { userName });
                if(result.AsList().Count == 0)
                {
                    throw new KeyNotFoundException(); 
                }
                return MapUserItem(result.FirstOrDefault());
            }
        }
        private UserProfileDTO MapUserItem(dynamic result)
        {
            var dto = new UserProfileDTO
            {
                UserName = result.username,
                District = result.district,
                Email = result.email,
                EmailConfirmed = result.emailconfirmed,
                Name = result.name,
                Phone = result.phone,
                Street = result.street,
                Ward = result.ward
            };
            return dto;
        }
    }
}
