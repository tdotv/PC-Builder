using Microsoft.AspNetCore.Mvc;
using PC_Designer.Shared;

namespace PC_Designer.Server.Controllers;

[ApiController]
[Route("api/sockets")]
public class SocketController : ControllerBase
{
    private readonly ISocketService _socketService;

    public SocketController(ISocketService socketService) { _socketService = socketService; }

    [HttpGet]
    public async Task<ActionResult<List<Sockets>>> GetSockets()
    {
        var sockets = await _socketService.GetSocketsAsync();
        return Ok(sockets); 
    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreateSockets(Sockets sockets)
    {
        var success = await _socketService.CreateSocketsAsync(sockets);
        return Ok(success);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Sockets>> UpdateSockets(int id, Sockets sockets)
    {
        if (id !=  sockets.SocketId) { return BadRequest("InvalidId"); }

        var updatedSocket = await _socketService.UpdateSocketsAsync(sockets);

        if (updatedSocket != null) { return Ok(updatedSocket); }
        else { return NotFound("Socket not found"); }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteSockets(int id)
    {
        var success = await _socketService.DeleteSocketsAsync(id);
        
        if (success) { return Ok(success); }
        else { return NotFound("Socket not found"); }
    }
}
