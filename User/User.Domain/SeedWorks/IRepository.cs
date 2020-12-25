using System;
using System.Collections.Generic;
using System.Text;

namespace User.Domain.SeedWorks
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
