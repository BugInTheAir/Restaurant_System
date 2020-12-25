using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.Aggregate.UserAggregate;
using User.Domain.SeedWorks;
using User.Domains.Aggregate.UserAggregate;

namespace User.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public void Update(UserAccount uAccount)
        {
            _context.Entry(uAccount).State = EntityState.Modified;
        }
    }
}
