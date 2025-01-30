using Carneirofc.Scaffold.Application.Contracts.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carneirofc.Scaffold.Application.Services
{
    public class WeatherReportService : IWeatherReportService
    {
        private readonly ILogger<WeatherReportService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherReportService(ILogger<WeatherReportService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        async public Task<string> GetWeatherReport(string city, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("City is required", nameof(city));
            }
            _logger.BeginScope("GetWeatherReport");
            _logger.LogInformation($"Getting weather report for city '{city}'");
            using var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://wttr.in/{city}?format=j1", cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw new ValidationException($"City '{city}' not found");
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}
