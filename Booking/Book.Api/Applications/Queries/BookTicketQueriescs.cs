using Book.Api.Applications.Models;
using Dapper;
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
        private List<UserBookTicketViewModel> ToBookTicket(dynamic obj)
        {
            var bookTickets = new List<UserBookTicketViewModel>();
            foreach(var item in obj)
            {
                string status = "";
                if (item.IsCanceled)
                {
                    status = "Canceled";
                }
                else if (item.IsFinished)
                {
                    status = "Finished";
                }
                else
                {
                    status = "Pending";
                }
                bookTickets.Add(new UserBookTicketViewModel
                {
                    AtDate = $"{item.CreatedDate}-{item.BookInfo_AtHour.ToString()}:{item.BookInfo_AtMinute.ToString()}",
                    Status = status,
                    TicketId = item.BookId,
                    UserName = item.BookerInf_UserName
                });
            }
            return bookTickets;
        }
        public async Task<List<UserBookTicketViewModel>> GetUserBookTickets(string userName)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"select b.BookerInf_UserName, bt.BookId, bt.CreatedDate, bt.IsCanceled, bt.IsFinished, bt.ResId, bt.BookInfo_AtHour, bt.BookInfo_AtMinute, bt.BookInfo_Note from dbo.BookTicket bt inner join dbo.Booker b on bt.BookerId = b.BookerId where b.IsAnnonymous = 0";
                    var result = await connection.QueryAsync<dynamic>(query);
                    var parseList = ToBookTicket(result);
                    parseList = parseList.Where(x => x.UserName == userName).ToList();
                    return parseList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
