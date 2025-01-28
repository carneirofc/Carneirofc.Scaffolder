using Carneirofc.Scaffold.Infrastructure.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Carneirofc.Scaffold.Infrastructure.BackgroundServices
{
    public class StartupBackgroundService : BackgroundService
    {
        private readonly ILogger<StartupBackgroundService> _logger;
        private readonly ReadyHealthCheck _healthCheck;

        public StartupBackgroundService(ILogger<StartupBackgroundService> logger, ReadyHealthCheck healthCheck)
        {
            _logger = logger;
            _healthCheck = healthCheck;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("StartupBackgroundService is starting.");
            // Simulate the effect of a long-running task.
            await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);

            _healthCheck.Complete();
            _logger.LogInformation("StartupBackgroundService has completed.");
        }
    }
}