using PC_Designer.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace PC_Designer.Server.Controllers
{
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

        [HttpPost("loginuser")]
        public async Task<ActionResult<User>> LoginUser(User user, bool isPersistent)
        {
            // user.Password = Utility.Encrypt(user.Password);
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
                await HttpContext.SignInAsync(claimsPrincipal, GetAuthenticationProperties(isPersistent));
            }

            return await Task.FromResult(loggedInUser);
        }

        [HttpGet("getcurrentuser")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            User currentUser = new User();

            if (User.Identity.IsAuthenticated)
            {
                // currentUser.EmailAddress = User.FindFirstValue(ClaimTypes.Name);

                string query = "SELECT * FROM dbo.Users WHERE EmailAddress=@EmailAddress";
                var emailAddress = User.FindFirstValue(ClaimTypes.Name);   
                var parameters = new { EmailAddress = emailAddress };
                
                currentUser = await dbService.GetAsync<User>(query, parameters);
            }

            return await Task.FromResult(currentUser);
        }

        [HttpGet("logoutuser")]
        public async Task<ActionResult<String>> LogOutUser()
        {
            await HttpContext.SignOutAsync();
            return "Success";
        }

        // [HttpPost("vklogin")]
        // public async Task<ActionResult<VkUserViewModel>> VkLogin(string accessToken, int vkUserId)
        // {
        //     try
        //     {
        //         VkUserViewModel vkUser = await VkAuthService.AuthenticateWithVkAsync(accessToken, vkUserId);
        //         return Ok(vkUser); 
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest($"Error during VK authentication: {ex.Message}");
        //     }
        // }

        public AuthenticationProperties GetAuthenticationProperties(bool isPersistent = false)
        {
            return new AuthenticationProperties()
            {
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.Now.AddMinutes(10),
                RedirectUri = "/profile",
            };
        }

        [HttpGet("notauthorized")]
        public IActionResult NotAuthorized()
        {
            return Unauthorized();
        }
    }
}