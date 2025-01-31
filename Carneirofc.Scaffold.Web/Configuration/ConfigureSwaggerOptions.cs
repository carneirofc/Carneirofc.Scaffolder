using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Carneirofc.Scaffold.Web.Configuration
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
          this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            var openApiInfo = static () => new OpenApiInfo()
            {
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

            };

            foreach (var description in provider.ApiVersionDescriptions)
            {
                var info = openApiInfo();
                if (description.IsDeprecated)
                {
                    info.Description += " This API version has been deprecated.";
                }

                info.Title = $"Installers Service API {description.ApiVersion}";
                info.Version = description.ApiVersion.ToString();
                options.SwaggerDoc(description.GroupName, info);
            }

            // Get the XML documentation file for the current project
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        }
    }
}