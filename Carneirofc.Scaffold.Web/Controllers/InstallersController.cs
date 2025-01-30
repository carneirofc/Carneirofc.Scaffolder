using System.Net.Mime;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Carneirofc.Scaffold.Domain.Models;
using Carneirofc.Scaffold.Application.Contracts.Services;
using Microsoft.FeatureManagement;
using Carneirofc.Scaffold.Web.Controllers.DTO;

namespace Carneirofc.Scaffold.Web.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class InstallersController : ControllerBase
    {
        private readonly ILogger<InstallersController> _logger;
        private readonly IInstallerService _installerService;
        private readonly IFeatureManager _featureManager;

        public InstallersController(ILogger<InstallersController> logger, IInstallerService installerService, IFeatureManager featureManager)
        {
            _logger = logger;
            _installerService = installerService;
            _featureManager = featureManager;
        }

        /// <summary>
        /// Get information about installers.
        /// </summary>
        /// <returns>An enumerable list of installers.</returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async IAsyncEnumerable<Installer> GetInstallers(InstallerQueryDto query)
        {
            await foreach (var e in _installerService.List(query.Path, query.Filter))
            {
                yield return e;
            }
        }
    }
}