using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PC_Designer.Shared;

namespace PC_Designer.Server.Controllers;

[ApiController]
[Route("api/configuration")]
// [Authorize]
public class PcConfigurationController : ControllerBase
{
    private readonly IPcConfigurationService _configurationService;

    public PcConfigurationController(IPcConfigurationService configurationService) { _configurationService = configurationService; }

    [HttpGet]
    public async Task<ActionResult<List<PcConfigurations>>> GetConfigurations()
    {
        var configurations = await _configurationService.GetConfigurationsAsync();
        return Ok(configurations); 
    }
}
