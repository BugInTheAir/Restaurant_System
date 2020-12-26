using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Restaurant.Api.Infrastructure.AutofacModules;
using Restaurant.Infrastructure;

namespace Restaurant.Api
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
            builder.RegisterModule(new AutofacApplicationModule(Configuration.GetConnectionString("RestaurantDb")));
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RestaurantContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("RestaurantDb"),
              sqlServerOptionsAction: sqlOptions =>
              {
                  sqlOptions.MigrationsAssembly("Restaurant.Infrastructure");
                  sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
              }));
            services.AddOptions();
            services.AddControllers();
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
                    builder.RequireScope("restaurant.api.admin");
                });
                options.AddPolicy("User", builder =>
                {
                    builder.RequireScope("restaurant.api.user");
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
