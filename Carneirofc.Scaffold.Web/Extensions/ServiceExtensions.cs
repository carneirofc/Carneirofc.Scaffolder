using Asp.Versioning;
using Carneirofc.Scaffold.Application.Contracts.Services;
using Carneirofc.Scaffold.Application.Contracts.UseCases.Installers;
using Carneirofc.Scaffold.Application.Services;
using Carneirofc.Scaffold.Application.UseCases.Installers;
using Carneirofc.Scaffold.Infrastructure.BackgroundServices;
using Carneirofc.Scaffold.Infrastructure.HealthChecks;
using Carneirofc.Scaffold.Web.Configuration;
using Carneirofc.Scaffold.Web.Validators;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using FluentValidation;

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
            services.AddScoped<IInstallersListUseCase, InstallersListUseCase>();
            services.AddScoped<IInstallerService, InstallerService>();
            services.AddScoped<IWeatherReportService, WeatherReportService>();

            services.AddValidatorsFromAssemblyContaining<InstallerQueryValidator>();
        }

        public static void AddCustomSwagger(this IServiceCollection services)
        {

#if NET9_0_OR_GREATER
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            services.AddOpenApi();
#endif

            // https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-8.0&tabs=visual-studio-code#add-and-configure-swagger-middleware

            services.AddProblemDetails();
            services.AddEndpointsApiExplorer();
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
            })
                .AddMvc()
                .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
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