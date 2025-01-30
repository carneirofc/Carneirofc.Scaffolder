using Microsoft.AspNetCore.HttpLogging;

namespace Carneirofc.Scaffold.Web.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddConfiguration(this WebApplicationBuilder builder, string[] args)
        {
            var env = builder.Environment;
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);
        }


        public static void AddCustomLogging(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Logging.AddSimpleConsole(options =>
            {
                options.IncludeScopes = true;
                options.SingleLine = true;
                options.TimestampFormat = "hh:mm:ss ";
            });

#if false
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
#endif

        }
    }
}