using System.Net.Mime;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Carneirofc.Scaffold.Domain.Models;
using Carneirofc.Scaffold.Application.Contracts.Services;
using Microsoft.FeatureManagement;
using Carneirofc.Scaffold.Web.Controllers.DTO;
using FluentValidation;
using Asp.Versioning;
using Carneirofc.Scaffold.Application.Contracts.UseCases.Installers;

namespace Carneirofc.Scaffold.Web.Controllers
{

    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/[controller]")]
    public class InstallersController : ControllerBase
    {
        private readonly IValidator<InstallerQueryDto> _validator;
        private readonly IInstallersListUseCase _installersListUseCase;

        public InstallersController(ILogger<InstallersController> logger, IInstallerService installerService, IFeatureManager featureManager, IValidator<InstallerQueryDto> validator, IInstallersListUseCase installersListUseCase)
        {
            _validator = validator;
            _installersListUseCase = installersListUseCase;
        }

        /// <summary>
        /// Get information about installers.
        /// </summary>
        /// <returns>An enumerable list of installers.</returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async IAsyncEnumerable<Installer> GetInstallers([FromQuery] InstallerQueryDto query, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(query);
            var items = await _installersListUseCase.Execute(new() { Filter = query.Filter ?? "", Path = query.Path }, cancellationToken);
            await foreach (var e in items)
            {
                yield return e;
            }
        }
    }
}