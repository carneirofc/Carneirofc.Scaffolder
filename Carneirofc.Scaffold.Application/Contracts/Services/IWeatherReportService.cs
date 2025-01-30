using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carneirofc.Scaffold.Application.Contracts.Services
{
    public interface IWeatherReportService
    {
        public Task<string> GetWeatherReport(string city, CancellationToken cancellationToken);
    }
}
