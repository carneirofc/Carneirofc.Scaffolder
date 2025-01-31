using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Carneirofc.Scaffold.Web.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void Configure(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseHsts();
            app.UseCors(configurePolicy =>
            {
                configurePolicy.AllowAnyOrigin();
                configurePolicy.AllowAnyMethod();
                configurePolicy.AllowAnyHeader();
            });
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
                app.UseSwaggerUI(options =>
                {
                    foreach (var description in app.DescribeApiVersions())
                    {
                        var url = $"/swagger/{description.GroupName}/swagger.json";
                        var name = description.GroupName.ToUpperInvariant();
                        options.SwaggerEndpoint(url, name);
                    }
                });

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