using Carneirofc.Scaffold.Domain.Models;

namespace Carneirofc.Scaffold.Application.Contracts.Services
{
    public interface IInstallerService
    {
        public IAsyncEnumerable<Installer> List(string path, string? filter);
    }
}