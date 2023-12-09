using Microsoft.AspNetCore.Mvc;
using PC_Designer.Shared;

namespace PC_Designer.Server.Controllers;

[ApiController]
[Route("api/cpu")]
// [Authorize]
public class CpuController : ControllerBase
{
    private readonly IPcConfigurationService _configurationService;

    public CpuController(IPcConfigurationService configurationService) 
    { 
        _configurationService = configurationService; 
    }

    [HttpGet]
    public async Task<ActionResult<List<CPUs>>> GetCpusAsync()
    {
        var cpus = await _configurationService.GetCpusAsync();
        return Ok(cpus); 
    }
}