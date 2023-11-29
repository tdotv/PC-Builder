using System.Net.Http.Json;
using PC_Designer.Shared;

namespace PC_Designer.ViewModels
{
    public class RegisterViewModel : IRegisterViewModel
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string ReenterPassword { get; set; }

        private readonly HttpClient _httpClient;

        public RegisterViewModel()
        {

        }

        public RegisterViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task RegisterUser()
        {
            Console.WriteLine(this.EmailAddress);
            Console.WriteLine(this.Password);
            await _httpClient.PostAsJsonAsync<User>("user/registeruser", this);
        }

        public static implicit operator RegisterViewModel(User user)
        {
            return new RegisterViewModel
            {
                EmailAddress = user.EmailAddress,
                Password = user.Password
            };
        }

        public static implicit operator User(RegisterViewModel registerViewModel)
        {
            return new User
            {
                EmailAddress = registerViewModel.EmailAddress,
                Password = registerViewModel.Password
            };
        }
    }
}