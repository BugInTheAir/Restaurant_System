using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using User.Api.Infrastructure.AutofacModules;
using User.Api.Infrastructure.Configuration;
using User.Domains.Aggregate.UserAggregate;
using User.Infrastructure;

namespace User_Service
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            builder.RegisterModule(new AutofacMediatorModule());
            builder.RegisterModule(new AutofacApplicationModule(Configuration.GetConnectionString("UserDb")));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddOptions();

            services.AddDbContext<UserContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("UserDb"),
                 sqlServerOptionsAction: sqlOptions =>
                 {
                     sqlOptions.MigrationsAssembly("User.Infrastructure");
                     sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                 }));

            services.AddIdentity<UserAccount, IdentityRole>().AddEntityFrameworkStores<UserContext>().AddDefaultTokenProviders();

            services.AddIdentityServer(
                    options =>
                    {

                        options.IssuerUri = "http://25.81.40.169:5000";
                    }
                )
                .AddAspNetIdentity<UserAccount>()
               .AddDeveloperSigningCredential()
               .AddInMemoryPersistedGrants()
               .AddInMemoryApiResources(Config.GetApis())
               .AddInMemoryClients(Config.GetClients())
               .AddInMemoryApiScopes(Config.GetScopes())
               .AddInMemoryIdentityResources(Config.GetIdentityResources())
               .AddProfileService<IdentityProfileService>();

            services.AddAuthentication("Bearer").AddIdentityServerAuthentication("Bearer", options =>
            {
                options.Authority = "http://25.81.40.169:5000";
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", builder =>
                {
                    builder.RequireScope("user.api.admin");
                });
                options.AddPolicy("User", builder =>
                {
                    builder.RequireScope("user.api.user");
                });
            });
          
     
        }
    
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
