using Autofac;
using DataCenter.Api.Applications.Commands;
using DataCenter.Api.Applications.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DataCenter.Api.Infrastructures.AutofacModules
{
    public class AutofacMediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();
            //Register commands
            builder.RegisterAssemblyTypes(typeof(UploadImageCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.Register(c => new SirvUploader()).As<IUploadImageService>();
            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

        }
    }
}
