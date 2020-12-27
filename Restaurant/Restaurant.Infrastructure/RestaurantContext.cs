using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Restaurant.Domain.Aggregates.MenuAggregate;
using Restaurant.Domain.Aggregates.RestaurantAggregate;
using Restaurant.Infrastructure.EntityTypeConfigs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Restaurant.Domain.Seedwork;
using Restaurant.Domain.Aggregates.Common;
using Restaurant.Domain.Aggregates.FoodAggregate;
using Restaurant.Infrastructure.EntityTypeConfigs.Restaurant;

namespace Restaurant.Infrastructure
{
    public class RestaurantContext : DbContext, IUnitOfWork
    {
        public static string DEFFAULT_SCHEMA = "dbo";
        public DbSet<Restaurants> Restaurants { get; set; }
        public DbSet<RestaurantAndMenu> RestaurantAndMenus { get; set; }
        public DbSet<FoodAndMenu> FoodAndMenus { get; set; }
        public DbSet<FoodItem> FoodItems{ get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<ResAndType> ResAndTypes { get; set; }
        public DbSet<RestaurantType> RestaurantTypes { get; set; }
        private readonly IMediator _mediator;
        public RestaurantContext(DbContextOptions<RestaurantContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RestaurantTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantAndMenuEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FoodEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MenuEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FoodAndMenuEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantImageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ResAndTypeEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _mediator.DispatchDomainEventsAsync(this);
                var result = await base.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
    }
    public class OrderingContextDesignFactory : IDesignTimeDbContextFactory<RestaurantContext>
    {
        public RestaurantContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RestaurantContext>()
                .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection=True;");

            return new RestaurantContext(optionsBuilder.Options, new NoMediator());
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
                throw new NotImplementedException();
            }
        }
    }
}
