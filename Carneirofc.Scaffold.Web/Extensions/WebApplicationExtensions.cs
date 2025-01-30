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

#if false
            app.UseW3CLogging();
#endif

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

#if NET9_0_OR_GREATER
                // Configure the HTTP request pipeline.
                app.MapOpenApi();
#endif
            }

            app.MapControllers();
            app.UseHttpsRedirection();
        }
    }
}