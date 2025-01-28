using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace Carneirofc.Scaffold.Infrastructure.HealthChecks
{
    public class HealthCheckPublisher : IHealthCheckPublisher
    {
        private readonly ILogger<HealthCheckPublisher> _logger;
        public HealthCheckPublisher(ILogger<HealthCheckPublisher> logger)
        {
            _logger = logger;
        }
        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            if (report.Status == HealthStatus.Healthy)
            {
                // ...
            } else
            {
                // ...
                report.Entries.ToList().ForEach(e =>
                {
                    if (e.Value.Status == HealthStatus.Unhealthy)
                    {
                        _logger.LogWarning($"Health check {e.Key} is unhealthy: {e.Value.Description}.");
                        // ...
                    }
                    // ...
                });
            }

            return Task.CompletedTask;
        }
    }
}