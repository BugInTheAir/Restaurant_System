using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using User.Api.Application.Queries;
using User.Domain.Aggregate.UserAggregate;
using User.Infrastructure.ExternalIntegrationService;
using User.Infrastructure.Repositories;

namespace User.Api.Infrastructure.AutofacModules
{
    public class AutofacApplicationModule : Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public AutofacApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new UserQueries(QueriesConnectionString)).As<IUserQueries>();
            builder.RegisterType<UserRepository>()
               .As<IUserRepository>()
               .InstancePerLifetimeScope();
            builder.RegisterType<ExternalService>()
             .As<IExternalService>()
             .InstancePerLifetimeScope();


        }
    }
}
