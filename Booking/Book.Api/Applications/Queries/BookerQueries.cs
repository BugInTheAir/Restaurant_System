using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Api.Applications.Queries
{
    public class BookerQueries : IBookerQueries
    {
        private string _connectionString;

        public BookerQueries(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<string> GetBookerEmail(string bookerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"select BookerInf_Email from dbo.Booker where BookerId = @bookerId";
                return (await connection.QueryAsync(query, new { bookerId })).FirstOrDefault().BookerInf_Email;
            }
            throw new NotImplementedException();
        }
    }
}
