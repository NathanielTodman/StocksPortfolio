using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StocksPortfolio.Entities;
using Microsoft.EntityFrameworkCore;
using StocksPortfolio.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace StocksPortfolio
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddSingleton(Configuration);
            services.AddMvc()
                .AddMvcOptions(o=> o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()))
                ;
            var connectionString = Configuration["ConnectionStrings:FoxConnection"];
            services.AddDbContext<FoxContext>(o=>o.UseSqlServer(connectionString));
            services.AddIdentity<FoxUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
                cfg.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
            })
            .AddEntityFrameworkStores<FoxContext>()
            .AddDefaultTokenProviders();

            services.AddScoped < SignInManager<FoxUser>>();
            services.AddScoped<IFoxStocksRepository, FoxStocksRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, FoxContext foxContext)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            
            app.UseStaticFiles();

            loggerFactory.AddDebug();

            app.UseIdentity();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            foxContext.EnsureSeedDataForContext();


            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Entities.FoxUser, Models.UserDTO>();
                config.CreateMap<Entities.Transactions, Models.TransactionDTO>();
                config.CreateMap<Models.CreateTransactionDTO, Entities.Transactions>();
                config.CreateMap<Models.CreateUserDTO, Entities.FoxUser>();
                config.CreateMap<Models.UpdateUserDTO, Entities.FoxUser>();
                config.CreateMap<Entities.FoxUser, Models.UpdateUserDTO>();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" }
                );
            });
        }
    }
}
