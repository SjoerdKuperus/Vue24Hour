using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vue24Hour.Domain.Repository;
using Vue24Hour.Persistence;
using Vue24Hour.Services;

namespace Vue24Hour
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            // Services
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IGameService, GameService>();
            // Repositories
            services.AddSingleton<IGameRepository, DomainContext>();
            services.AddSingleton<ITeamRepository, DomainContext>();
            services.AddSingleton<IAccountRepository, DomainContext>();
            // TimeHostedService
            services.AddHostedService<TimedHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //TODO dit werkt nog wel maar later vervangen : (https://github.com/aspnet/AspNetCore/issues/12890)
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapFallbackToController("Index", "Home");
            });


            
        }
    }
}
