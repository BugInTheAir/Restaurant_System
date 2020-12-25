using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.SeedWorks;
using User.Domains.Aggregate.UserAggregate;
using User.Infrastructure.MediatorExtension;

namespace User.Infrastructure
{
    public class UserContext : IdentityDbContext<UserAccount, IdentityRole, string>, IUnitOfWork
    {
        private readonly IMediator _mediator;
        public UserContext(DbContextOptions options) : base(options)
        {
        }
        public UserContext(DbContextOptions options,IMediator mediator): base(options)
        {
            _mediator = mediator;
        }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);

            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
        public class UserContextDesignFactory : IDesignTimeDbContextFactory<UserContext>
        {
            public UserContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<UserContext>()
                    .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=UserDb;Trusted_Connection=True;");

                return new UserContext(optionsBuilder.Options, new NoMediator());
            }

            class NoMediator : IMediator
            {
                public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
                {
                    return Task.CompletedTask;
                }

                public Task Publish(object notification, CancellationToken cancellationToken = default)
                {
                    return Task.CompletedTask;
                }

                public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
                {
                    return Task.FromResult<TResponse>(default(TResponse));
                }

                public Task<object> Send(object request, CancellationToken cancellationToken = default)
                {
                    throw new System.NotImplementedException();
                }
            }
        }
    }
}
