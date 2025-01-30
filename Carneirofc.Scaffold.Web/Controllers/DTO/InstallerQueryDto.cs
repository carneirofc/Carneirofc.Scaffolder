using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Carneirofc.Scaffold.Web.Controllers.DTO
{
    public class InstallerQueryDto
    {
        [FromQuery(Name = "path")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Path is required")]
        public required string Path { get; init; }

        [FromQuery(Name = "filter")]
        public string? Filter { get; init; }
    }
}