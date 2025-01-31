using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Carneirofc.Scaffold.Web.Controllers.DTO
{
    public class InstallerQueryDto
    {
        required public string Path { get; init; }

        required public string Filter { get; init; } = "*";
    }
}