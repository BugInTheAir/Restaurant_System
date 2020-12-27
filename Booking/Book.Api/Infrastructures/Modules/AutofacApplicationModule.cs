using Autofac;
using Book.Api.Applications.Queries;
using Book.Domain.Aggregates;
using Book.Domain.Aggregates.BookingAggregate;
using Book.Infrastructure.BookRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Api.Infrastructures.Modules
{
    public class AutofacApplicationModule : Module
    {
        private readonly string _connectionString = String.Empty;

        public AutofacApplicationModule(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(_connectionString));
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new BookTicketQueriescs(_connectionString)).As<IBookTicketQueries>();
            builder.RegisterType<BookerRepository>().As<IBookerRepository>();
            builder.RegisterType<BookTicketRepository>().As<IBookingRepository>();
        }
    }
}
