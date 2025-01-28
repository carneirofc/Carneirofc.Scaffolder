using Microsoft.AspNetCore.HttpLogging;

namespace Carneirofc.Scaffold.Web.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddCustomLogging(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddW3CLogging(logging =>
            {
                // Log all W3C fields
                logging.LoggingFields = W3CLoggingFields.All;

                logging.AdditionalRequestHeaders.Add("x-forwarded-for");
                logging.AdditionalRequestHeaders.Add("x-client-ssl-protocol");
                logging.FileSizeLimit = 5 * 1024 * 1024;
                logging.RetainedFileCountLimit = 2;
                // logging.FileName = "wc3log-";
                // logging.LogDirectory = @"C:\logs";
                logging.FlushInterval = TimeSpan.FromSeconds(2);
            });

        }
    }
}