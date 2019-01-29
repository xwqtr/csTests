using CurrencyService.DAL;
using CurrencyService.DB;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CurrencyService.WebApi.Models;
using CurrencyService.DB.Models;
using System;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;
using CurrencyService.WebApi.Models.Chart;
using Microsoft.AspNet.OData.Builder;
using CurrencyService.Common.Interfaces;
using Microsoft.AspNet.OData.Query;

namespace CurrencyService.WebApi
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
            services.AddIdentity<ApplicationUser, Role>()
                .AddEntityFrameworkStores<CurrencyDbContext>()
                .AddDefaultTokenProviders();
            services
                .AddCors()
                .AddScoped<DbReadService>()
                .AddDbContext<CurrencyDbContext>(x => x.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Currencies;Trusted_Connection=True;"))
                .AddLogging(x => x.AddConsole().AddDebug())
                .AddMvc(x => x.EnableEndpointRouting = false)
                
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<IdentityOptions>(options =>
            {
                // User settings
                
                options.User.RequireUniqueEmail = true;
            });
            services.AddResponseCompression();
            services.AddOData();
            services
                .AddAuthentication()
                .AddCookie(options => {
                    options.LoginPath = "/Account/LogIn";
                    options.LogoutPath = "/Account/LogOut";
                    
                })
                .AddGoogle(options =>
                {
                    //options.CallbackPath = "/api/currencies";
                    options.ClientId = "136049929047-igjqan27r3acnfuieg6mt3aeuhvtrsq8.apps.googleusercontent.com";// Configuration["auth:google:clientid"];
                    options.ClientSecret = "ftOex-i_BJInczpMFU0o7ikr";// Configuration["auth:google:clientsecret"];
                });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseResponseCompression();
            app.UseCookiePolicy();
            app.UseCors(x=>x.WithOrigins("http://localhost:4200").AllowCredentials());
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc(x=>x.MapODataServiceRoute("ODataRoutes", "odata",GetEdmModel(app.ApplicationServices)));
            
        }


        private static IEdmModel GetEdmModel(IServiceProvider sp) {
            var builder = new ODataConventionModelBuilder();
            
            builder.EntitySet<HistoricalTrade>(nameof(HistoricalTrade))
                .EntityType
                .Filter()
                .Count()
                .Expand()
                .OrderBy()
                .Page()
                .Select();

            //var f = builder.Function("GetHistoricalTradesModel");
            return builder.GetEdmModel();
        }
    }
}
