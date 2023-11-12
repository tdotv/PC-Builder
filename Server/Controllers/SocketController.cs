using Microsoft.AspNetCore.Mvc;

namespace PC_Designer.Server.Controllers;

[ApiController]
[Route("api/sockets")]
public class SocketController : ControllerBase
{
    private readonly ISocketService _socketService;

    public SocketController(ISocketService socketService) { _socketService = socketService; }

    [HttpGet]
    public async Task<IActionResult> GetSockets()
    {
        var sockets = await _socketService.GetSocketsAsync();
        return Ok(sockets); 
    }
}
