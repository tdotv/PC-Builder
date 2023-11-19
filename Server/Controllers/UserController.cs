using PC_Designer.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> logger;
    private readonly IDbService dbService;

    public UserController(ILogger<UserController> logger, IDbService dbService)
    {
        this.logger = logger;
        this.dbService = dbService;
    }

    [HttpPut("updateprofile/{userId}")]
    public async Task<User> UpdateProfile(int userId, [FromBody] User user)
    {
        User userToUpdate = await dbService.GetAsync<User>("SELECT * FROM Users WHERE UserId = @UserId", new { UserId = userId });

        if (userToUpdate != null)
        {
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.EmailAddress = user.EmailAddress;

            await dbService.Update<User>("UPDATE Users SET FirstName=@FirstName, LastName=@LastName, EmailAddress=@EmailAddress WHERE UserId=@UserId", userToUpdate);
        }

        return user;
    }

    [HttpGet("getprofile/{userId}")]
    public async Task<User> GetProfile(int userId)
    {
        return await dbService.GetAsync<User>("SELECT * FROM Users WHERE UserId = @UserId", new { UserId = userId });
    }
}