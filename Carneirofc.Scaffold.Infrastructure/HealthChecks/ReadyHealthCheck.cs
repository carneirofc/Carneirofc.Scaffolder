using Carneirofc.Scaffold.Application.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;


namespace Carneirofc.Scaffold.Infrastructure.HealthChecks
{
    public class ReadyHealthCheck : IHealthCheck
    {
        private volatile bool _isReady;
        private readonly ILogger<ReadyHealthCheck> _logger;

        public ReadyHealthCheck(ILogger<ReadyHealthCheck> logger)
        {
            _logger = logger;
        }

        public bool StartupCompleted
        {
            get => _isReady;
            set => _isReady = value;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (StartupCompleted)
            {
                _logger.LogDebug("Startup task is completed.");
                return Task.FromResult(HealthCheckResult.Healthy("The startup task has completed."));
            }
            _logger.LogError("Startup task is still running.");
            return Task.FromResult(HealthCheckResult.Unhealthy("That startup task is still running."));
        }

        public void Complete()
        {
            StartupCompleted = true;
        }
    }
}