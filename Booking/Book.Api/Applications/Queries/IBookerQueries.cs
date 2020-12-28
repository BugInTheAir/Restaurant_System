using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Api.Applications.Queries
{
    public interface IBookerQueries
    {
        public Task<string> GetBookerEmail(string bookerId);
    }
}
