using Carneirofc.Scaffold.Domain.Models;

namespace Carneirofc.Scaffold.Application.Contracts.Services
{
    public interface IInstallerService
    {
        public IAsyncEnumerable<Installer> Get(string path, string? filter);
    }
}