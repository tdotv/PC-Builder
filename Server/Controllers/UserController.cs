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
    private readonly IDbService dbService;

    public UserController(IDbService dbService)
    {
        this.dbService = dbService;
    }

    [HttpGet("getprofile/{userId}")]
    public async Task<User> GetProfile(int userId)
    {
        return await dbService.GetAsync<User>("SELECT * FROM dbo.Users WHERE UserId = @UserId", new { UserId = userId });
    }

    [HttpPut("updateprofile/{userId}")]
    public async Task<User> UpdateProfile(int userId, [FromBody] User user)
    {
        User userToUpdate = await dbService.GetAsync<User>("SELECT * FROM dbo.Users WHERE UserId = @UserId", new { UserId = userId });

        if (userToUpdate != null)
        {
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.EmailAddress = user.EmailAddress;
            userToUpdate.AboutMe = user.AboutMe;

            await dbService.Update<User>("UPDATE dbo.Users SET FirstName=@FirstName, LastName=@LastName, EmailAddress=@EmailAddress, AboutMe=@AboutMe WHERE UserId=@UserId",
             userToUpdate);
        }

        return user;
    }

    [HttpGet("updatetheme")]
    public async Task<User> UpdateTheme(string userId, string DarkTheme)
    {            
        try
        {
            int userIdInt = Convert.ToInt32(userId);
            await dbService.Update<User>("UPDATE dbo.Users SET DarkTheme=@DarkTheme WHERE UserId=@UserId", 
                new { UserId = userIdInt, DarkTheme = DarkTheme == "True" ? 1 : 0});
            return await dbService.GetAsync<User>("SELECT * FROM dbo.Users WHERE UserId=@UserId", new { UserId = userIdInt });
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("updatenotifications")]
    public async Task<User> UpdateNotifications(string userId, string Notifications)
    {
        try
        {
            int userIdInt = Convert.ToInt32(userId);
            await dbService.Update<User>("UPDATE dbo.Users SET Notifications=@Notifications WHERE UserId=@UserId", 
                new { UserId = userIdInt, Notifications = Notifications == "True" ? 1 : 0});
            return await dbService.GetAsync<User>("SELECT * FROM dbo.Users WHERE UserId=@UserId", new { UserId = userIdInt });
        }
        catch (Exception)
        {
            throw;
        }
    }
}