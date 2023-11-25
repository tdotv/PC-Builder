using PC_Designer.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

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

    //Authentication Methods
    [HttpPost("loginuser")]
    public async Task<ActionResult<User>> LoginUser(User user)
    {
        string query = "SELECT * FROM dbo.Users WHERE EmailAddress=@EmailAddress AND Password=@Password";
        var parameters = new { EmailAddress = user.EmailAddress, Password = user.Password };
        User loggedInUser = await dbService.GetAsync<User>(query, parameters);

        if (loggedInUser != null)
        {
            //create a claim
            var claim = new Claim(ClaimTypes.Name, loggedInUser.EmailAddress);
            //create claimsIdentity
            var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");
            //create claimsPrincipal
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            //Sign In User
            await HttpContext.SignInAsync(claimsPrincipal);
        }

        return await Task.FromResult(loggedInUser);
    }

    [HttpGet("getcurrentuser")]
    public async Task<ActionResult<User>> GetCurrentUser()
    {
        User currentUser = new User();

        if (User.Identity.IsAuthenticated)
        {
            currentUser.EmailAddress = User.FindFirstValue(ClaimTypes.Name);
        }

        return await Task.FromResult(currentUser);
    }

    [HttpGet("logoutuser")]
    public async Task<ActionResult<String>> LogOutUser()
    {
        await HttpContext.SignOutAsync();
        return "Success";
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