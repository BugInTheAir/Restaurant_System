using Autofac;
using DataCenter.Api.Applications.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCenter.Api.Infrastructures.AutofacModules
{
    public class AutofacApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(i => new SirvUploader()).As<IUploadImageService>();
        }
    }
}
