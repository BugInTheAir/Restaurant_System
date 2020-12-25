using Autofac;
using Email.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Email.Api.Modules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailService>()
             .As<IEmailService>()
             .InstancePerLifetimeScope();
        }
    }
}
