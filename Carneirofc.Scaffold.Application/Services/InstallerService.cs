using Carneirofc.Scaffold.Application.Contracts.Services;
using Carneirofc.Scaffold.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Carneirofc.Scaffold.Application.Services
{

    public class InstallerService : IInstallerService
    {
        private readonly ILogger<InstallerService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public InstallerService(ILogger<InstallerService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        async public IAsyncEnumerable<Installer> List(string path, string? filter)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path is required", nameof(path));
            }
            var dinfo = new DirectoryInfo(path);
            if (!dinfo.Exists)
            {
                // return empty
                yield break;
                //throw new DirectoryNotFoundException($"Path '{path}' not found");
            }
            if (filter is null)
            {
                filter = "*";
            }
            var files = dinfo.GetFiles(filter, SearchOption.TopDirectoryOnly);
            foreach (var f in files)
            {
                yield return new Installer
                {
                    Name = f.Name,
                    FileName = f.FullName,
                    Size = f.Length
                };
            }
        }
    }
}