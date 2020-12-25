using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.SeedWorks;
using User.Domains.Aggregate.UserAggregate;

namespace User.Domain.Aggregate.UserAggregate
{
    public interface IUserRepository : IRepository<UserAccount>
    {
        void Update(UserAccount uAccount);

    }
}
