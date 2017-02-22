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
            services.AddMvc();
            var connectionString = Configuration["ConnectionStrings:FoxConnection"];
            services.AddDbContext<FoxContext>(o=>o.UseSqlServer(connectionString));
            // Set simple passwords
            services.AddIdentity<FoxUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
                cfg.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
                cfg.Password.RequireDigit = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequiredLength = 5;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<FoxContext>()
            .AddDefaultTokenProviders();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("*")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });
            services.AddScoped < SignInManager<FoxUser>>();
            services.AddScoped<IFoxStocksRepository, FoxStocksRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, FoxContext foxContext)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            
            app.UseStaticFiles();

            loggerFactory.AddDebug();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseCors("AllowSpecificOrigin");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //foxContext.EnsureSeedDataForContext();

            // for mapping from model to viewmodel and vice versa
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Entities.Transactions, Models.TransactionDTO>();
                config.CreateMap<Entities.Transactions, ViewModels.TransactionModel>();
                config.CreateMap<ViewModels.TransactionModel, Entities.Transactions>();
                config.CreateMap<Models.CreateTransactionDTO, Entities.Transactions>();
                config.CreateMap<Models.TransactionDTO, Entities.Portfolio>();
                config.CreateMap<Entities.Portfolio, Models.PortfolioDTO>();
            });

            app.UseIdentity();

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
