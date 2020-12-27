using Autofac;
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
            //builder.Register(c => new RestaurantQueries(_connectionString)).As<IRestaurantQueries>();

            builder.RegisterType<BookerRepository>().As<IBookerRepository>();
            builder.RegisterType<BookTicketRepository>().As<IBookingRepository>();
        }
    }
}
