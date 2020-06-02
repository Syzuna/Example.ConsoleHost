using System.Threading;
using System.Threading.Tasks;
using Example.ConsoleHost.Models.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Example.ConsoleHost
{
    public class LifetimeMonitoringHostedService : IHostedService
    {
        private readonly ILogger<LifetimeMonitoringHostedService> _logger;
        private readonly DiscordOptions _discordOptions;
        private readonly TwitchOptions _twitchOptions;

        public LifetimeMonitoringHostedService(ILogger<LifetimeMonitoringHostedService> logger, IOptions<DiscordOptions> discordOptions, IOptions<TwitchOptions> twitchOptions)
        {
            _logger = logger;
            _discordOptions = discordOptions.Value;
            _twitchOptions = twitchOptions.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Application started");
            _logger.LogInformation($"{string.Join(',', _discordOptions.Channels)}");
            _logger.LogInformation($"{_twitchOptions.Credentials.ClientId}");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning("Application shutting down");

            return Task.CompletedTask;
        }
    }
}