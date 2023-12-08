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

    // [HttpPost]
    // public async Task<ActionResult<bool>> CreateConfigurations(PcConfigurations configurations)
    // {
    //     var success = await _configurationService.CreateConfigurationsAsync(configurations);
    //     return Ok(success);
    // }

    // [HttpPut("{id}")]
    // public async Task<ActionResult<PcConfigurations>> UpdateConfigurations(int id, PcConfigurations configurations)
    // {
    //     if (id !=  configurations.PcConfigurationId) { return BadRequest("InvalidId"); }

    //     var updatedConfiguration = await _configurationService.UpdateConfigurationsAsync(configurations);

    //     if (updatedConfiguration != null) { return Ok(updatedConfiguration); }
    //     else { return NotFound("Configuration not found"); }
    // }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteConfigurations(int id)
    {
        var success = await _configurationService.DeleteConfigurationsAsync(id);
        
        if (success) { return Ok(success); }
        else { return NotFound("Configuration not found"); }
    }
}
