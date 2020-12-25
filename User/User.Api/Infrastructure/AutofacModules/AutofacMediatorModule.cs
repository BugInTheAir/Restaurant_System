using Autofac;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using User.Api.Application.Behaviors;
using User.Api.Application.Commands;
using User.Api.Application.DomainEventHandler;
using User.Api.Application.Validations;

namespace User.Api.Infrastructure.AutofacModules
{
    public class AutofacMediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();
            //Register commands
            builder.RegisterAssemblyTypes(typeof(CreateUserCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            //Register domain events
            builder.RegisterAssemblyTypes(typeof(ValidateEmailDomainEventHandler).GetTypeInfo().Assembly)
               .AsClosedTypesOf(typeof(INotificationHandler<>));

            //Register Validators
            builder
                .RegisterAssemblyTypes(typeof(UserRegisterValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();
      

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });


            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

        }
    }
}
