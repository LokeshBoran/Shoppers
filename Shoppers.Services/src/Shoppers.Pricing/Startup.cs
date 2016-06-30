using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shoppers.Core.Data;
using Shoppers.Core.Rest.Catalogue;
using Shoppers.Pricing.Data;

namespace Shoppers.Pricing
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PricingContext>();
            services.Add(new ServiceDescriptor(typeof(IRepository<>), typeof(Repository<>), ServiceLifetime.Transient));
            services.AddTransient<ICoreDbContext, PricingContext>();
            services.AddTransient<UnitOfWork>();
            services.AddSingleton( typeof(IConfigurationRoot), Configuration);

            services.AddTransient<ICatalogueWebRepository,CatalogueWebRepository>();

            // Add framework services.
            services.AddMvc();

             services.AddCors(o => o.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors("AllowAll");
            app.UseMvc();
            
        }
    }
}
