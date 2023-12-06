using PC_Designer.Shared;
using PC_Designer.Shared.Extensions;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PC_Designer.Shared.Services;
using System.Threading.Tasks;

#nullable disable

namespace PC_Designer.ViewModels
{
    public class ProfileViewModel : IProfileViewModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string AboutMe { get; set; }
        public byte[] ProfilePictureData { get; set; }
        public bool Notifications { get; set; }
        public bool DarkTheme { get; set; }

        public bool IsEditing { get; set; }
        private readonly HttpClient _httpClient;
        private readonly IAccessTokenService _accessTokenService;

        public ProfileViewModel()
        { }

        public ProfileViewModel(HttpClient httpClient, IAccessTokenService accessTokenService) 
        { 
            _httpClient = httpClient;
            _accessTokenService = accessTokenService;
        }

        public async Task UpdateProfile()
        {
            User user = this;
            await _httpClient.PutAsJsonAsync("profile/updateprofile/" + this.UserId, user);

            // var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            // await _httpClient.PutAsync<User>($"profile/updateprofile/{UserId}", this, jwtToken);
        }

        public async Task UpdateProfileWithImage(byte[] imageBytes)
        {
            User user = this;
            user.ProfilePictureData = imageBytes;

            await _httpClient.PutAsJsonAsync("profile/updateprofile/" + this.UserId, user);
        }

        public async Task GetProfile()
        {
            ProfileViewModel profile = await _httpClient.GetFromJsonAsync<User>($"profile/getprofile/{UserId}");
            if (profile != null)
            {
                LoadCurrentObject(profile);
            }
        }

        public async Task Save()
        {   
            try
            {
                await _httpClient.GetFromJsonAsync<User>($"profile/updatetheme?userId={this.UserId}&DarkTheme={this.DarkTheme.ToString()}");
                await _httpClient.GetFromJsonAsync<User>($"profile/updatenotifications?userId={this.UserId}&Notifications={this.Notifications.ToString()}");
            }
            catch (HttpRequestException httpEx) { Console.WriteLine($"HTTP Request Error: {httpEx.Message}"); }
            catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
        }

        public async Task UpdateTheme()
        {
            await _httpClient.GetFromJsonAsync<User>($"profile/updatetheme?userId={UserId}&DarkTheme={DarkTheme}");
        }

        public async Task UpdateNotifications()
        {
            await _httpClient.GetFromJsonAsync<User>($"profile/updatenotifications?userId={UserId}&Notifications={Notifications}");
        }

        private void LoadCurrentObject(ProfileViewModel profileViewModel)
        {
            FirstName = profileViewModel.FirstName;
            LastName = profileViewModel.LastName;
            EmailAddress = profileViewModel.EmailAddress;
            AboutMe = profileViewModel.AboutMe;
            ProfilePictureData = profileViewModel.ProfilePictureData;
            DarkTheme = profileViewModel.DarkTheme;
            Notifications = profileViewModel.Notifications;
        }

        public static implicit operator ProfileViewModel(User user)
        {
            return new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                AboutMe = user.AboutMe,
                ProfilePictureData = user.ProfilePictureData,
                DarkTheme = user.DarkTheme != null && (long)user.DarkTheme != 0,
                Notifications = user.Notifications != null && (long)user.Notifications != 0,
                UserId = user.UserId
            };
        }

        public static implicit operator User(ProfileViewModel profileViewModel)
        {
            return new User
            {
                FirstName = profileViewModel.FirstName,
                LastName = profileViewModel.LastName,
                EmailAddress = profileViewModel.EmailAddress,
                AboutMe = profileViewModel.AboutMe,
                ProfilePictureData = profileViewModel.ProfilePictureData,
                DarkTheme = profileViewModel.DarkTheme ? 1 : 0,
                Notifications = profileViewModel.Notifications ? 1 : 0,
                UserId = profileViewModel.UserId
            };
        }
    }
}
