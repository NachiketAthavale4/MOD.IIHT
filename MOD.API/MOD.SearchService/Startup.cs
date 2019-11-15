using System;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MOD.SearchService.Contexts;
using MOD.SearchService.Infrastructure;
using MOD.SearchService.Repositories;
using NLog.Extensions.Logging;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace MOD.SearchService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<SearchContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("SearchDbStr")));
            services.AddTransient<ISearchRepository, SearchRepository>();

            services.AddSingleton<IHostedService, ConsulHostedService>();
            services.Configure<ConsulConfig>(
                Configuration.GetSection("consulConfig"));
            services.AddTransient<IConsulClient, ConsulClient>(
                p => new ConsulClient(
                consulConfig =>
                {
                    var address = Configuration["consulConfig:address"];
                    consulConfig.Address = new Uri(address);
                }
            ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature =
                            context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            var logger =
                                loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500, exceptionHandlerFeature.Error,
                                exceptionHandlerFeature.Error.Message);
                        }
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync(
                            "An unexpected fault happened. Try again later.");
                    });
                });
                app.UseHsts();
            }

            app.UseMvc();
        }
    }
}
