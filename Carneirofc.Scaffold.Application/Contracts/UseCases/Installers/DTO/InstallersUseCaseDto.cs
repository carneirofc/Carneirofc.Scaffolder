using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carneirofc.Scaffold.Application.Contracts.UseCases.Installers.DTO
{
    public class InstallersUseCaseParamsDto
    {
        required public string Path { get; init; }
        required public string Filter { get; init; }
    }
}
