using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Carneirofc.Scaffold.Web.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void Configure(this WebApplication app)
        {
            app.MapHealthChecks("/healthz/ready", new HealthCheckOptions
            {
                Predicate = healthCheck => healthCheck.Tags.Contains("ready")
            });

            app.MapHealthChecks("/healthz/live", new HealthCheckOptions
            {
                Predicate = healthCheck => healthCheck.Tags.Contains("live")
            });

            app.UseW3CLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // Configure the HTTP request pipeline.
                app.MapOpenApi();
            }

            app.MapControllers();
            app.UseHttpsRedirection();
        }
    }
}