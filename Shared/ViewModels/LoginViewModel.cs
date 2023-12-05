using System.Net.Http.Json;
using PC_Designer.Shared;
using System.Net.Http.Headers;
using System.Text.Json;

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
            await _httpClient.PostAsJsonAsync<User>($"user/loginuser?isPersistent={RememberMe}", this);
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

        public async Task<User> GetUserByJWTAsync(string jwtToken)
        {
            try
            {
                var jsonToken = JsonSerializer.Serialize(jwtToken);
                

                //preparing the http request
                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "user/getuserbyjwt")
                {
                    Content = new StringContent(jwtToken)
                    {
                        
                        Headers =
                        {
                            ContentType = new MediaTypeHeaderValue("application/json")
                        }
                    }
                };

                // var requestMessage = new HttpRequestMessage(HttpMethod.Post, "user/getuserbyjwt");
                // requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                //making the http request
                var response = await _httpClient.SendAsync(requestMessage);

                //returning the user if found
                var returnedUser = await response.Content.ReadFromJsonAsync<User>();
                if (returnedUser != null) return await Task.FromResult(returnedUser);
                else return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.GetType());
                return null;
            }            
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