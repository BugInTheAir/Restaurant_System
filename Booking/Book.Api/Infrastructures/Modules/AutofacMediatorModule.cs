using Autofac;
using Book.Api.Applications.Behaviors;
using Book.Api.Applications.Commands;
using Book.Api.Applications.Validations;
using Book.Domain.Events;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Book.Api.Infrastructures.Modules
{
    public class AutofacMediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();
            //Register commands
            builder.RegisterAssemblyTypes(typeof(CreateBookingTicketCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            //Register domain events
            builder.RegisterAssemblyTypes(typeof(BookTicketCreatedDomainEvent).GetTypeInfo().Assembly)
               .AsClosedTypesOf(typeof(INotificationHandler<>));

            //Register Validators
            builder
                .RegisterAssemblyTypes(typeof(CreateBookingTicketCommandValidator).GetTypeInfo().Assembly)
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
