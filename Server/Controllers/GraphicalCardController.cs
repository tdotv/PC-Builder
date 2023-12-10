using Microsoft.AspNetCore.Mvc;
using PC_Designer.Shared;

namespace PC_Designer.Server.Controllers;

[ApiController]
[Route("api/graphicalcard")]
public class GraphicalCardController : ControllerBase
{
    private readonly IPcConfigurationService _configurationService;

    public GraphicalCardController(IPcConfigurationService configurationService) 
    { 
        _configurationService = configurationService; 
    }

    [HttpGet]
    public async Task<ActionResult<List<GraphicalCards>>> GetConfigurations()
    {
        var graphicalCards = await _configurationService.GetGraphicalCardsAsync();
        return Ok(graphicalCards); 
    }
}