using PC_Designer.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PC_Designer.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IDbService dbService;
        private readonly IConfiguration configuration;

        public UserController(ILogger<UserController> logger, IDbService dbService, IConfiguration configuration)
        {
            this.logger = logger;
            this.dbService = dbService;
            this.configuration = configuration;
        }

        // [HttpPost("loginuser")]
        // public async Task<ActionResult<User>> LoginUser(User user, bool isPersistent)
        // {
        //     try
        //     {
        //         user.Password = Utility.Encrypt(user.Password);
        //         string query = "SELECT TOP 1 * FROM dbo.Users WHERE EmailAddress=@EmailAddress AND Password=@Password";
        //         var parameters = new { EmailAddress = user.EmailAddress, Password = user.Password };
        //         User loggedInUser = await dbService.GetAsync<User>(query, parameters);

        //         if (loggedInUser != null)
        //         {
        //             //create a claim
        //             var claimEmail = new Claim(ClaimTypes.Email, loggedInUser.EmailAddress);
        //             var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, loggedInUser.UserId.ToString());
        //             //create claimsIdentity
        //             var claimsIdentity = new ClaimsIdentity(new[] { claimEmail, claimNameIdentifier }, "serverAuth");
        //             //create claimsPrincipal
        //             var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        //             //Sign In User
        //             await HttpContext.SignInAsync(claimsPrincipal, GetAuthenticationProperties(isPersistent));
        //         }

        //         return await Task.FromResult(loggedInUser);
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine("Exception: " + ex.Message);
        //         return null;
        //     }
        // }

        [HttpGet("getcurrentuser")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            User currentUser = new ();

            if (User.Identity.IsAuthenticated)
            {
                string query = "SELECT * FROM dbo.Users WHERE EmailAddress=@EmailAddress";
                var emailAddress = User.FindFirstValue(ClaimTypes.Name);   
                var parameters = new { EmailAddress = emailAddress };
                
                currentUser = await dbService.GetAsync<User>(query, parameters);

                if (currentUser == null)
                {                  
                    currentUser = new()
                    {
                        EmailAddress = User.FindFirstValue(ClaimTypes.Email)
                    };
                    currentUser.Password = Utility.Encrypt(currentUser.EmailAddress);
                    currentUser.Source = "EXTL";

                    await dbService.Insert<User>("INSERT INTO dbo.Users (EmailAddress, Password, Source) VALUES (@EmailAddress, @Password, @Source)",
                        new { EmailAddress = currentUser.EmailAddress, Password = currentUser.Password, Source = currentUser.Source });
                }
            }

            return await Task.FromResult(currentUser);
        }

        [HttpPost("registeruser")]
        public async Task<ActionResult> RegisterUser(User user)
        {
            //  In this method you should only create a user record and not authenticate the user

            var emailAddressExists = await dbService.GetAsync<User>("SELECT TOP 1 * FROM dbo.Users WHERE EmailAddress=@EmailAddress", 
                new { EmailAddress = user.EmailAddress });

            if (emailAddressExists == null)
            {
                user.Password = Utility.Encrypt(user.Password);
                user.Source = "APPL";

                await dbService.Insert<User>("INSERT INTO dbo.Users (EmailAddress, Password, Source) VALUES (@EmailAddress, @Password, @Source)", user);
            }
            return Ok();
        }

        // [HttpGet("logoutuser")]
        // public async Task<ActionResult<String>> LogOutUser()
        // {
        //     await HttpContext.SignOutAsync();
        //     return "Success";
        // }

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
                // ExpiresUtc = DateTime.Now.AddMinutes(10),
                RedirectUri = "/profile",
            };
        }

        [HttpGet("notauthorized")]
        public IActionResult NotAuthorized()
        {
            return Unauthorized();
        }

        //Migrating to JWT Authorization...
        private string GenerateJwtToken(User user)
        {
            //getting the secret key
            string secretKey = configuration["JWTSettings:SecretKey"];
            var key = Encoding.ASCII.GetBytes(secretKey);
        
            //create claims
            var claimEmail = new Claim(ClaimTypes.Email, user.EmailAddress);
            var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString());
        
            //create claimsIdentity
            var claimsIdentity = new ClaimsIdentity(new[] { claimEmail, claimNameIdentifier }, "serverAuth");
        
            // generate token that is valid for 7 days
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //creating a token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
        
            //returning the token back
            return tokenHandler.WriteToken(token);
        }

        [HttpPost("authenticatejwt")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateJWT(AuthenticationRequest authenticationRequest)
        {
            string token = string.Empty;
        
            //checking if the user exists in the database
            authenticationRequest.Password = Utility.Encrypt(authenticationRequest.Password);
            User loggedInUser = await dbService.GetAsync<User>("SELECT TOP 1 * FROM dbo.Users WHERE EmailAddress=@EmailAddress AND Password=@Password",
                new { EmailAddress = authenticationRequest.EmailAddress, Password = authenticationRequest.Password });
        
            if (loggedInUser != null)
            {
                //generating the token
                token = GenerateJwtToken(loggedInUser);
            }
            return await Task.FromResult(new AuthenticationResponse() { Token = token });
        }

        [HttpPost("getuserbyjwt")]
        public async Task<ActionResult<User>> GetUserByJWT([FromBody] string jwtToken)
        {
            try
            {
                //getting the secret key
                string secretKey = configuration["JWTSettings:SecretKey"];
                var key = Encoding.ASCII.GetBytes(secretKey);
        
                //preparing the validation parameters
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
        
                //validating the token
                var principle = tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = (JwtSecurityToken)securityToken;
        
                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    //returning the user if found
                    var userId = principle.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    return await dbService.GetAsync<User>("SELECT TOP 1 * FROM dbo.Users WHERE UserId=@UserId",
                        new { UserId = Convert.ToInt64(userId) });
                }
            }
            catch (Exception ex)
            {
                //logging the error and returning null
                Console.WriteLine("Exception : " + ex.Message);
                return null;
            }
            //returning null if token is not validated
            return null;
        }
    }
}