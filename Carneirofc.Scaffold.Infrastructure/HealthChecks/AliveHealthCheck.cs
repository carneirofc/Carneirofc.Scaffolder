using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace Carneirofc.Scaffold.Infrastructure.HealthChecks
{
    public class AliveHealthCheck : IHealthCheck
    {
        private readonly ILogger<AliveHealthCheck> _logger;
        public AliveHealthCheck(ILogger<AliveHealthCheck> logger)
        {
            _logger = logger;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = true;

            // ...

            if (isHealthy)
            {
                _logger.LogDebug("Alive check is healthy.");
                return Task.FromResult(
                    HealthCheckResult.Healthy("A healthy result."));
            }

            _logger.LogError("Alive check is unhealthy.");
            return Task.FromResult(
                new HealthCheckResult(
                    context.Registration.FailureStatus, "An unhealthy result."));
        }
    }
}