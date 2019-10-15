using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vue24Hour.Services;

namespace Vue24Hour
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions => { })
                        .UseStartup<Startup>();
                })
                .ConfigureServices(services =>
                {
                    // TimeHostedService
                    services.AddHostedService<TimedHostedService>();
                });
    }
}
