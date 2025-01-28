using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Carneirofc.Scaffold.Domain.Models;
using Carneirofc.Scaffold.Application.Contracts.Services;

namespace Carneirofc.Scaffold.Web.Controllers
{
    public class InstallerQuery
    {
        [FromQuery(Name = "path")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Path is required")]
        public required string Path { get; set; }

        [FromQuery(Name = "filter")]
        public string? Filter { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class InstallersController : ControllerBase
    {
        private readonly ILogger<InstallersController> _logger;
        private readonly IInstallerService _installerService;

        public InstallersController(ILogger<InstallersController> logger, IInstallerService installerService)
        {
            _logger = logger;
            _installerService = installerService;
        }

        /// <summary>
        /// Get information about installers.
        /// </summary>
        /// <returns>An enumerable list of installers.</returns>
        [HttpGet()]
        [Produces(MediaTypeNames.Application.Json)]
        public async IAsyncEnumerable<Installer> GetInstallers(InstallerQuery query)
        {
            await foreach (var e in _installerService.Get(query.Path, query.Filter))
            {
                yield return e;
            }
        }
    }
}