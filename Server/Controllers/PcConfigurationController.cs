using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PC_Designer.Shared;
using PC_Designer.ViewModels;

namespace PC_Designer.Server.Controllers;

[ApiController]
[Route("api/configuration")]
// [Authorize]
public class PcConfigurationController : ControllerBase
{
    private readonly IPcConfigurationService _configurationService;
    private readonly IMapper _mapper;

    public PcConfigurationController(IPcConfigurationService configurationService, IMapper mapper) 
    { 
        _configurationService = configurationService; 
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<PcConfigurations>>> GetConfigurations()
    {
        var configurations = await _configurationService.GetConfigurationsAsync();
        return Ok(configurations); 
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateConfigurations(PcConfigurations configuration)   //  [FromBody]
    {
        var viewModel = _mapper.Map<PcConfigurationViewModel>(configuration);
        var id = await _configurationService.CreateConfigurationsAsync(viewModel);
        return Ok(id);
    }

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
