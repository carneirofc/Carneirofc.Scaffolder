using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carneirofc.Scaffold.Application.Contracts.UseCases.Installers.DTO;
using Carneirofc.Scaffold.Domain.Models;

namespace Carneirofc.Scaffold.Application.Contracts.UseCases.Installers
{
    public interface IInstallersListUseCase
    {
        Task<IAsyncEnumerable<Installer>> Execute(InstallersUseCaseParamsDto p, CancellationToken cancellationToken);
    }
}
