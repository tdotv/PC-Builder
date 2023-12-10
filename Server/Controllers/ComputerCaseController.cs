using Microsoft.AspNetCore.Mvc;
using PC_Designer.Shared;

namespace PC_Designer.Server.Controllers;

[ApiController]
[Route("api/computercase")]
public class ComputerCaseController : ControllerBase
{
    private readonly IPcConfigurationService _configurationService;

    public ComputerCaseController(IPcConfigurationService configurationService) 
    { 
        _configurationService = configurationService; 
    }

    [HttpGet]
    public async Task<ActionResult<List<ComputerCases>>> GetComputerCasesAsync()
    {
        var cases = await _configurationService.GetComputerCasesAsync();
        return Ok(cases); 
    }
}