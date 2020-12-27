using Book.Api.Applications.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Api.Applications.Queries
{
    public class BookTicketQueriescs : IBookTicketQueries
    {
        private readonly string _connectionString;

        public BookTicketQueriescs(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<UserBookTicketViewModelcs> GetUserBookTickets(string userName)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"";
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
