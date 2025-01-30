using Carneirofc.Scaffold.Web.Controllers.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

namespace Carneirofc.Scaffold.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfoController : ControllerBase
    {
        private readonly IFeatureManager _featureManager;
        private readonly ILogger<InfoController> _logger;
        private readonly IConfiguration _configuration;
        public InfoController(ILogger<InfoController> logger, IFeatureManager featureManager, IConfiguration configuration)
        {
            _featureManager = featureManager;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<InfoResponseDto>> Get()
        {
            var features = new Dictionary<string, bool>();
            await foreach (var k in _featureManager.GetFeatureNamesAsync())
            {
                features.Add(k, await _featureManager.IsEnabledAsync(k));
            }

            var buildInfo = _configuration.GetSection("Metadata:Build").Get<BuildInfo>();

            var info = new InfoResponseDto
            {
                Features = features,
                Metadata = new InfoMetadataDto()
                {
                    Build = buildInfo

                }
            };
            return Ok(info);
        }
    }

}