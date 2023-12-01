using PC_Designer.Shared;

namespace PC_Designer.ViewModels
{
    public interface ILoginViewModel
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public Task LoginUser();
        public Task<AuthenticationResponse> AuthenticateJWT();
        public Task<User> GetUserByJWTAsync(string jwtToken);
    }
}