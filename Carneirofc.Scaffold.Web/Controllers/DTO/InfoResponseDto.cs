using System.Reflection;

namespace Carneirofc.Scaffold.Web.Controllers.DTO
{
    public class BuildInfo
    {
        [ConfigurationKeyName("Build.BuildNumber")]
        required public string Number { get; init; }
        [ConfigurationKeyName("Build.SourceBranch")]
        required public string SourceBranch { get; init; }
        [ConfigurationKeyName("Build.SourceVersion")]
        required public string SourceVersion { get; init; }
        [ConfigurationKeyName("Build.Repository.Uri")]
        required public string RepositoryUri { get; init; }
        [ConfigurationKeyName("Build.BuildId")]
        required public string BuildId { get; init; }
    }

    public class InfoMetadataDto
    {
        public string Name { get; init; } = Assembly.GetEntryAssembly()?.GetName().Name ?? "Unknown";
        public string Version { get; init; } = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "Unknown";
        public string Description { get; init; } = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description ?? "Unknown";
        required public BuildInfo Build { get; init; }

    }

    public class InfoResponseDto
    {
        required public InfoMetadataDto Metadata { get; init; }
        required public IDictionary<string, bool> Features { get; init; }
    }
}