using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(config =>
                {
                    //We check the cookie to confirm that we are authenticated
                    config.DefaultAuthenticateScheme = "ClientCookie";
                    //When we sign in we will deal out a cookie
                    config.DefaultSignInScheme = "ClientCookie";
                    //Use this to check if we are allowed to do something
                    config.DefaultChallengeScheme = "OurServer";
                })
                .AddCookie("ClientCookie")
                .AddOAuth("OurServer", config =>
                {
                    config.CallbackPath = "/oauth/callback";
                    config.ClientId = "client_id";
                    config.ClientSecret = "client_secret";
                    config.AuthorizationEndpoint = "http://localhost:12258/oauth/authorize";
                    config.TokenEndpoint = "http://localhost:12258/oauth/token";

                    config.SaveTokens = true;
                });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}