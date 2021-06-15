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
using System.IdentityModel.Tokens.Jwt;

namespace MvcClient
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
            //用来指定 InboundClaimTypeMap 是否启用。这个类似乎找不到？
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddControllersWithViews();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies"; //默认的方案是Cookie
                    options.DefaultChallengeScheme = "oidc"; //默认的挑战方案是open id connect
                })
                //应对上面的默认方案
                .AddCookie("Cookies")
                //应对上面的默认挑战方案
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "https://localhost:5001";

                    options.ClientId = "mvc";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";

                    //需要存储tokens到cookie中。默认是false，为了减小cookie的大小
                    options.SaveTokens = true;
                    
                    options.Scope.Add("profile");
                    options.GetClaimsFromUserInfoEndpoint = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute().RequireAuthorization();
                // endpoints.MapControllerRoute(
                //     name: "default",
                //     pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}