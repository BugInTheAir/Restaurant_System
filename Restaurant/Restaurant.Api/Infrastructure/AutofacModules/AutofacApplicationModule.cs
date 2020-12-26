using Autofac;
using Restaurant.Api.Application.Queries;
using Restaurant.Domain.Aggregates.FoodAggregate;
using Restaurant.Domain.Aggregates.MenuAggregate;
using Restaurant.Domain.Aggregates.RestaurantAggregate;
using Restaurant.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Infrastructure.AutofacModules
{
    public class AutofacApplicationModule : Module
    {
        private readonly string _connectionString;
        public AutofacApplicationModule(string connStr)
        {
            _connectionString = connStr;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new RestaurantQueries(_connectionString)).As<IRestaurantQueries>();
            builder.RegisterType<RestaurantRepository>().As<IRestaurantRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MenuRepository>().As<IMenuRepository>().InstancePerLifetimeScope();
            builder.RegisterType<FoodItemRepository>().As<IFoodRepository>().InstancePerLifetimeScope();
        }
    }
}
