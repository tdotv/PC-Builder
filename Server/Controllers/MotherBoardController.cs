using PC_Designer.Shared;
using Microsoft.AspNetCore.Mvc;

namespace PC_Designer.Server.Controllers;

[ApiController]
[Route("api/motherboard")]
// [Authorize]
public class MotherBoardController : ControllerBase
{
    private readonly IPcConfigurationService _configurationService;

    public MotherBoardController(IPcConfigurationService configurationService) 
    { 
        _configurationService = configurationService; 
    }

    [HttpGet]
    public async Task<ActionResult<List<MotherBoards>>> GetMotherBoards()
    {
        var motherboards = await _configurationService.GetMotherBoardsAsync();
        return Ok(motherboards); 
    }
}