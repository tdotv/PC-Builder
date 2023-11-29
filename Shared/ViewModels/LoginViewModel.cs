using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using PC_Designer.Shared;

namespace PC_Designer.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        private readonly HttpClient _httpClient;

        public LoginViewModel()
        {

        }

        public LoginViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task LoginUser()
        {
            await _httpClient.PostAsJsonAsync<User>($"user/loginuser?isPersistent={this.RememberMe}", this);
        }

        public async Task<AuthenticationResponse> AuthenticateJWT()
        {
            //creating authentication request
            AuthenticationRequest authenticationRequest = new()
            {
                EmailAddress = this.EmailAddress,
                Password = this.Password
            };

            //authenticating the request
            var httpMessageReponse = await _httpClient.PostAsJsonAsync($"user/authenticatejwt", authenticationRequest);
            
            //sending the token to the client to store
            return await httpMessageReponse.Content.ReadFromJsonAsync<AuthenticationResponse>();
        }
        
        public async Task<string> GetFacebookAppIDAsync()
        {
            return await _httpClient.GetStringAsync("user/getfacebookappid");
        }

        public static implicit operator LoginViewModel(User user)
        {
            return new LoginViewModel
            {
                EmailAddress = user.EmailAddress,
                Password = user.Password
            };
        }

        public static implicit operator User(LoginViewModel loginViewModel)
        {
            return new User
            {
                EmailAddress = loginViewModel.EmailAddress,
                Password = loginViewModel.Password
            };
        }
    }
}