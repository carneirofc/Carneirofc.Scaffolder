using Carneirofc.Scaffold.Application.Contracts.Services;
using Carneirofc.Scaffold.Application.Services;
using Carneirofc.Scaffold.Infrastructure.BackgroundServices;
using Carneirofc.Scaffold.Infrastructure.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Carneirofc.Scaffold.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomInfrastructureServices(this IServiceCollection services)
        {
            services.AddHostedService<StartupBackgroundService>();
        }
        public static void AddCustomApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IInstallerService, InstallerService>();
            services.AddScoped<IWeatherReportService, WeatherReportService>();
        }

        public static void AddCustomSwagger(this IServiceCollection services)
        {
#if NET9_0_OR_GREATER
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            services.AddOpenApi();
#endif

            // https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-8.0&tabs=visual-studio-code#add-and-configure-swagger-middleware
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Installers Service API",
                    Description = "An ASP.NET Core Web API for managing local installers.",
                    TermsOfService = new Uri("https://www.gnu.org/licenses/old-licenses/lgpl-2.0.html#SEC1"),
                    Contact = new OpenApiContact
                    {
                        Name = "carneirofc",
                        Url = new Uri("https://github.com/carneirofc")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "LGPLv2.0",
                        Url = new Uri("https://www.gnu.org/licenses/old-licenses/lgpl-2.0.html#SEC1")
                    }
                });

                // Get the XML documentation file for the current project
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        public static void AddCustomHealthChecks(this IServiceCollection services)
        {
            services.AddSingleton<ReadyHealthCheck>();
            services.AddHealthChecks()
                 .AddCheck<ReadyHealthCheck>("Ready", tags: ["ready"]).
                 AddCheck<AliveHealthCheck>("Alive", tags: ["live"]);

            services.AddSingleton<IHealthCheckPublisher, HealthCheckPublisher>();
            services.Configure<HealthCheckPublisherOptions>(options =>
            {
                options.Delay = TimeSpan.FromSeconds(2);
                options.Predicate = _ => true;
            });
        }
    }
}