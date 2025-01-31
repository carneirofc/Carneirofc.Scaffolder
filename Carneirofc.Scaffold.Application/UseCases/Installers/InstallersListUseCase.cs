using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Carneirofc.Scaffold.Application.Contracts.Services;
using Carneirofc.Scaffold.Application.Contracts.UseCases.Installers;
using Carneirofc.Scaffold.Application.Contracts.UseCases.Installers.DTO;
using Carneirofc.Scaffold.Domain.Models;

namespace Carneirofc.Scaffold.Application.UseCases.Installers
{
    public class InstallersListUseCase : IInstallersListUseCase
    {
        private readonly IInstallerService _installerService;

        public InstallersListUseCase(IInstallerService installerService)
        {
            _installerService = installerService;
        }

        public async Task<IAsyncEnumerable<Installer>> Execute(InstallersUseCaseParamsDto p, CancellationToken cancellationToken)
        {
            return _installerService.List(path: p.Path, filter: p.Filter);
        }
    }
}
