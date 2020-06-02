using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Example.ConsoleHost.Models.Configuration;
using Microsoft.Extensions.Configuration;

namespace Example.ConsoleHost
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).RunConsoleAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), false);
                })
                .ConfigureServices((hostingContext, services) =>
                {
                    services.Configure<DiscordOptions>(hostingContext.Configuration.GetSection("Discord"));
                    services.Configure<TwitchOptions>(hostingContext.Configuration.GetSection("Twitch"));
                    services.AddHostedService<LifetimeMonitoringHostedService>();
                })
                .UseConsoleLifetime();
    }
}
